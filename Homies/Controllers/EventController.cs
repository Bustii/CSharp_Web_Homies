using Homies.Contracts;
using Homies.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace Homies.Controllers
{
    public class EventController :BaseController
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public async Task<IActionResult> All()
        {
            var model = await eventService.GetAllEventAsync();
            return View(model);
        }

        public async Task<IActionResult> Joined()
        {
            var model = await eventService.GetMyJoinedEventsAsync(GetUserId());

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddEventViewModel model = await eventService.GetNewAddEventModelAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEventViewModel model)
        {

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await eventService.AddEventAsync(model);

            return RedirectToAction(nameof(All));
        }
    }
}
