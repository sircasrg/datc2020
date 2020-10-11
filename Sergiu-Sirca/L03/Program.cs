using System;
using Google.Apis.Drive.v3;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Net;

namespace L03
{
    class Program
    {
        public static DriveService _service;
        public static string _token;
        static void Main(string[] args)
        {
            init();
            GetMyFiles();
            Upload().GetAwaiter().GetResult();
        }

        static void init(){
            string[] scopes = new string[]{
                DriveService.Scope.Drive,
                DriveService.Scope.DriveFile
            };

            var clientId = "532349387348-f17kmi69qjmapcj1s3losscjuq4p2pkq.apps.googleusercontent.com";
            var clientSecret = "F7wandapDcP0MAYs-_pprl32";

            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets{
                    ClientId = clientId,
                    ClientSecret = clientSecret
                },
                scopes,
                Environment.UserName,
                CancellationToken.None,
                null
            ).Result;

            _service = new DriveService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
            _token  = credential.Token.AccessToken;
            Console.WriteLine("Token: " +  _token);
        }
        static void GetMyFiles()
        {
            var request = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/drive/v3/files?q='root'%20in%20parents");
            request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + _token );
            using(var response = request.GetResponse())
            {
                using(Stream data = response.GetResponseStream())
                using (var reader = new StreamReader(data))
                {
                    string text = reader.ReadToEnd();
                    var myData = JObject.Parse(text);
                    foreach(var file in myData["files"])
                    {
                        if(file["mimeType"].ToString()!="application/vnd/google-apps.folder")
                        {
                            Console.WriteLine("File name: " + file["name"]);
                        }
                    }
                }
            }
        }

        public static async Task<Google.Apis.Drive.v3.Data.File> Upload(string documentId="root")
        {
            var name = ($"{DateTime.UtcNow.ToString()}.txt");
            var mimeType = "text/plain";

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = name,
                MimeType = mimeType,
                Parents = new[] { documentId }
            };

            FilesResource.CreateMediaUpload request;

            FileStream stream = new FileStream("stuff.txt", FileMode.Open, FileAccess.Read);
                request = _service.Files.Create(
                    fileMetadata, stream, mimeType
                );
                request.Fields = "id, name, parents, createdTime, modifiedTime, mimeType, thumbnailLink";
                await request.UploadAsync();
            return request.ResponseBody;
        }
    }
}