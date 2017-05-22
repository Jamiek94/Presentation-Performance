using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using WebPerformance.Services;

namespace WebPerformance.Controllers
{
    public class WebsiteController : Controller
    {
        private readonly IAzureQueueService _azureQueueService;

        public WebsiteController(IAzureQueueService azureQueueService)
        {
            _azureQueueService = azureQueueService;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(WebsiteViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool doHellOfALotRequests = false;

                Websites website;

                if (doHellOfALotRequests)
                {
                    website = await AddLotsOfItemsToQueue(model);
                }
                else
                {
                    website = await AddItemToQueue(model, 0);
                }


                return RedirectToAction("Processing", new {id = website.Id});
            }

            return View(model);
        }

        public IActionResult Processing(int id)
        {
            return View(id);
        }

        public IActionResult IsDone(int id)
        {
            var context = new PerformancePresentationContext();

            return Json(context.Websites.Any(w => w.Id == id && w.IsDone));
        }

        private async Task<Websites> AddLotsOfItemsToQueue(WebsiteViewModel model)
        {
            var websites = new Collection<Websites>();

            for (int i = 0; 50 > i; i++)
            {
                var website = await AddItemToQueue(model, i);

                websites.Add(website);
            }

            return websites.Last();
        }

        private async Task<Websites> AddItemToQueue(WebsiteViewModel model, int index)
        {
            using (var context = new PerformancePresentationContext())
            {
                var website = new Websites
                {
                    Name = model.Name + " - " + index,
                    Url = model.Url,
                    IsDone = false
                };

                context.Websites.Add(website);

                await context.SaveChangesAsync();

                await _azureQueueService.AddItemToQueue(website);

                return website;
            }
        }
    }
}