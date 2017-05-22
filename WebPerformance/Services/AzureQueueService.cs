using System.Threading.Tasks;
using Business;
using Database.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace WebPerformance.Services
{
    public class AzureQueueService : IAzureQueueService
    {
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudQueueClient _queueClient;
        private readonly CloudQueue _queue;

        public AzureQueueService()
        {
            _storageAccount = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
            
            _queueClient = _storageAccount.CreateCloudQueueClient();
            
            _queue = _queueClient.GetQueueReference("myqueue");
            
            _queue.CreateIfNotExistsAsync();
        }

        public async Task AddItemToQueue(Websites website)
        {
            var queueMessage = new CloudQueueMessage(GetJsonString(website));
            await _queue.AddMessageAsync(queueMessage);
        }

        private string GetJsonString(Websites website)
        {
            return JsonConvert.SerializeObject(website);
        }
    }
}
