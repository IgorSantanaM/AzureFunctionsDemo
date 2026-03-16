using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FunctionAppsDemo
{
    public class Function2
    {
        private readonly ILogger<Function2> _logger;

        public Function2(ILogger<Function2> logger)
        {
            _logger = logger;
        }

        [Function(nameof(Function2))] // Use the {queueTrigger} for using the same name of the object that was created in the queue
        public void Run([QueueTrigger("thequeue", Connection = "AzureWebJobsStorage")] QueueMessage message, 
            [Blob("theblobs/{queueTrigger}", FileAccess.Read, Connection = "AzureWebJobsStorage")] Stream myPic)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
            if (myPic != null)
                if (message.ToString().ToUpper().Contains("SECURITY"))
                    _logger.LogInformation($"Security Image found PicSize: " + myPic.Length + "bytes");
                else
                    _logger.LogInformation($"New Image found PicSize: " + myPic.Length + "bytes");
        }
    }
}
