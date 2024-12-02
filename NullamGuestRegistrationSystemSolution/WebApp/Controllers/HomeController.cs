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

            var vm = new ListOfEventVM();

            var pastEventsDb = await _uow.Events.GetAllPastEventsOrderedByNameAsync();
            var futureEventDb = await _uow.Events.GetAllFutureEventsOrderedByNameAsync();

            if (pastEventsDb != null)
            {
                var index = 0;
                foreach (var item in pastEventsDb)
                {
                    vm.PastEvents.Add(new IndexEventVM()
                    {
                        LineNumber = ++index,
                        Name = item!.Name,
                        Location = item.Location,
                        EventDateAndTime = item.EventDateAndTime,
                        NumberOfAttendeesPerEvent = item.NumberOfAttendees,
                        NumberOfAttendees = item.NumberOfAttendees,
                        AdditionalInfo = item.AdditionalInfo,
                        Id = item.Id
                    });
                }

            }

            if (futureEventDb != null)
            {
                var index = 0;
                foreach (var item in futureEventDb)
                {
                    vm.FutureEvents.Add(new IndexEventVM()
                    {
                        LineNumber = ++index,
                        Name = item!.Name,
                        Location = item.Location,
                        EventDateAndTime = item.EventDateAndTime,
                        NumberOfAttendeesPerEvent = item.NumberOfAttendees,
                        NumberOfAttendees = item.NumberOfAttendees,
                        AdditionalInfo = item.AdditionalInfo,
                        Id = item.Id
                    });
                }

            }

            return View(vm);
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