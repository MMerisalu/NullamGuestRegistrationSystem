using App.Contracts.DAL;
using App.DAL.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAppUnitOfWork _uow;
        

        public HomeController(ILogger<HomeController> logger, IAppUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
            
        }

        public async Task<IActionResult> Index()
        {
            var futureEventsDb = await _uow.Events.GetAllFutureEventsOrderedByNameAsync();
            var pastEventsDb = await _uow.Events.GetAllPastEventsOrderedByNameAsync();

            var futureEvents = futureEventsDb.Select(e => new IndexEventVM
            {
                LineNumber = 0, // Placeholder, calculate later
                Name = e.Name,
                EventDateAndTime = e.EventDateAndTime,
                Location = e.Location,
                AdditionalInfo = e.AdditionalInfo,
                NumberOfAttendees = _uow.Events.NumberOfAttendeesPerEvent(e.Id)
            }).ToList();

            var pastEvents = pastEventsDb.Select(e => new IndexEventVM
            {
                LineNumber = 0, // Placeholder, calculate later
                Name = e.Name,
                EventDateAndTime = e.EventDateAndTime,
                Location = e.Location,
                AdditionalInfo = e.AdditionalInfo,
                NumberOfAttendees = _uow.Events.NumberOfAttendeesPerEvent(e.Id)
            }).ToList();

            // Assign line numbers for ordering
            for (int i = 0; i < futureEvents.Count; i++) futureEvents[i].LineNumber = i + 1;
            for (int i = 0; i < pastEvents.Count; i++) pastEvents[i].LineNumber = i + 1;

            var model = new EventIndexVM
            {
                FutureEvents = futureEvents,
                PastEvents = pastEvents
            };

            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       
    }
}
