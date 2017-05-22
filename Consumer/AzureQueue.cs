using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Database.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace Consumer
{
    class AzureQueue
    {
        private readonly PerformancePresentationContext _databaseContext;
        private bool _isRunning;

        public AzureQueue(PerformancePresentationContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Start()
        {
            _isRunning = true;

            Console.WriteLine("Connecting to the Azure Queue...");

            var storageAccount = CloudStorageAccount.Parse("UseDevelopmentStorage=true;");

            var queueClient = storageAccount.CreateCloudQueueClient();

            var queue = queueClient.GetQueueReference("myqueue");

            var thread = new Thread(async () =>
            {
                await queue.CreateIfNotExistsAsync();
                await ProcessQueue(queue);
            }) { IsBackground = true };

            thread.Start();
        }

        public void Stop()
        {
            _isRunning = false;
        }

        private async Task ProcessQueue(CloudQueue queue)
        {
            string amountMessagesInQueue = await GetAmountMessagesInQueue(queue);

            Console.WriteLine($"Messages in queue: {amountMessagesInQueue}");

            while (_isRunning)
            {
                await Task.Delay(5000);

                Console.WriteLine("Checking queue...");

                var cloudQueueMessage = await queue.GetMessageAsync();

                if (cloudQueueMessage == null) { continue; }

                var website = JsonConvert.DeserializeObject<Websites>(cloudQueueMessage.AsString);

                Console.WriteLine($"Processing, id: {website.Id}, name: {website.Name}, url: {website.Url}");

                var dbTask = SetWebsiteToDoneInDb(website);

                var queueDeleteTask = queue.DeleteMessageAsync(cloudQueueMessage);

                await Task.WhenAll(dbTask, queueDeleteTask);
            }
        }

        private async Task SetWebsiteToDoneInDb(Websites website)
        {
           var dbWebsite = _databaseContext.Websites.Find(website.Id);

            if (dbWebsite != null)
            {
                dbWebsite.IsDone = true;
                await _databaseContext.SaveChangesAsync();
            }
        }

        private async Task<string> GetAmountMessagesInQueue(CloudQueue queue)
        {
            await queue.FetchAttributesAsync();

            return queue.ApproximateMessageCount.ToString() ?? "?";
        }
    }
}
