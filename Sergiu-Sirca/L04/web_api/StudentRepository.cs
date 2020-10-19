using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Models;
using  System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace web_api
{
    public class StudentRepository : IStudentRepository
    {
        private string _connectionString;
        private CloudTableClient _tableClient;
        private CloudTable _studentsTable;

        public StudentRepository(IConfiguration configuration)
        {
            _connectionString= configuration.GetValue<string>("AzureStorageAccountConnectionString");
            Task.Run(async () => { await InitializeTable(); })
            .GetAwaiter()
            .GetResult();
        }        public void CreateNewStudent(StudentEntity student)
        {
            throw new System.NotImplementedException();
        }


        public async Task<List<StudentEntity>> GetAllStudents()
        {
            var students = new List<StudentEntity>();

            TableQuery<StudentEntity> query =new TableQuery<StudentEntity>();
            TableContinuationToken token=null;
            do 
            {
                TableQuerySegment<StudentEntity> resultSegment = await _studentsTable.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;

                students.AddRange(resultSegment.Results);
            } while (token !=null);
            return students;
        }

        public async Task Create(StudentEntity student)
        {
            var insertOperation=TableOperation.Insert(student);
            await _studentsTable.ExecuteAsync(insertOperation);
        }

        public async Task Update (string partitionKey, string rowKey, StudentEntity student)
        {
            student.PartitionKey=partitionKey;
            student.RowKey=rowKey;
            student.ETag="*";
            TableOperation update = TableOperation.Replace(student);
            await _studentsTable.ExecuteAsync(update);
        }

        public async Task Delete(string partitionKey, string rowKey)
        {
            TableOperation retrieve =TableOperation.Retrieve<StudentEntity>(partitionKey, rowKey);
            TableResult result=await _studentsTable.ExecuteAsync(retrieve);

            StudentEntity deleteStudent=(StudentEntity)result.Result;
            TableOperation delete=TableOperation.Delete(deleteStudent);
            await _studentsTable.ExecuteAsync(delete);
        }
        private async Task InitializeTable() {
            var account = CloudStorageAccount.Parse(_connectionString);
            _tableClient = account.CreateCloudTableClient();

            _studentsTable = _tableClient.GetTableReference("studenti");
            await _studentsTable.CreateIfNotExistsAsync();
        }
    }
}