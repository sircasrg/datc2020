using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;

namespace DATC.Students
{
    public static class StudentsQueueTrigger
    {
        [FunctionName("StudentsQueueTrigger")]
        [return: Table("studenti")]
        public static StudentEntity Run([QueueTrigger("students-queue", Connection = "programusmecher_STORAGE")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"Queue trigger processed: {myQueueItem}");

            var student = JsonConvert.DeserializeObject<StudentEntity>(myQueueItem);

            return student;
        }
    }
}
