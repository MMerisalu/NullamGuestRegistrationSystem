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
            var eventsController = new EventsController(_uow);
            var eventsDb = await _uow.Events.GetAllEventsOrderedByNameAsync();
            var events = eventsController.CreateEventIndexVM(eventsDb);
            return View(events);
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
