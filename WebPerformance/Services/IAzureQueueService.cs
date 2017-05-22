using System.Threading.Tasks;
using Business;
using Database.Models;

namespace WebPerformance.Services
{
    public interface IAzureQueueService
    {
        Task AddItemToQueue(Websites website);
    }
}