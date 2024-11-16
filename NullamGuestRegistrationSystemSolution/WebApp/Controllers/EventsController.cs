using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using WebApp.ViewModels;
using System.Drawing.Text;
using App.Contracts.DAL.IAppRepositories;
using App.Contracts.DAL;
using App.DAL.DTO;
using Microsoft.Extensions.Logging;
using App.Enum;

namespace WebApp.Controllers
{
    public class EventsController : Controller
    {
        private readonly IAppUnitOfWork _uow;


        public EventsController(IAppUnitOfWork uow)
        {
            _uow = uow;

        }


        // GET: Events
        public async Task<IActionResult> Index()
        {

            var eventsDb = await _uow.Events.GetAllEventsOrderedByNameAsync();
            var events = CreateEventIndexVM(eventsDb);

            return View(events);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            var vm = new CreateEventVM();
            return View(vm);
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEventVM vm)
        {
            var newEvent = new EventDTO();
            newEvent.Name = vm.Name;
            newEvent.EventDateAndTime = DateTime.Parse(vm.EventDateAndTime);
            newEvent.Location = vm.Location;
            newEvent.AdditionalInfo = vm.AdditionalInfo;

            if (ModelState.IsValid)
            {
                _uow.Events.Add(newEvent);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index), controllerName: "Home");
            }
            return View(vm);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var vm = new EditEventVM();
            if (id == null)
            {
                return NotFound();
            }

            var eventdb = await _uow.Events.FirstOrDefaultAsync(id.Value);
            if (eventdb == null)
            {
                return NotFound();
            }
            vm.Id = eventdb.Id;
            vm.Name = eventdb.Name;
            vm.EventDateAndTime = eventdb.EventDateAndTime;
            vm.Location = eventdb.Location;

            return View(vm);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditEventVM vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }
            var eventDb = await _uow.Events.GetEventByIdAsync(id, noIncludes: true);
            if (eventDb == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    eventDb.Name = vm.Name;
                    eventDb.EventDateAndTime = vm.EventDateAndTime;
                    eventDb.Location = vm.Location;
                    eventDb.AdditionalInfo = vm.AdditionalInfo;
                    _uow.Events.Update(eventDb);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_uow.Events.Exists(eventDb.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var vm = new DeleteEventVM();
            if (id == null)
            {
                return NotFound();
            }

            var eventDb = await _uow.Events.FirstOrDefaultAsync(id.Value);
            if (eventDb == null)
            {
                return NotFound();
            }

            vm.Id = eventDb.Id;
            vm.Name = eventDb.Name;
            vm.EventDateAndTime = eventDb.EventDateAndTime;
            vm.Location = eventDb.Location;
            vm.AdditionalInfo = eventDb.AdditionalInfo;

            return View(vm);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventDb = await _uow.Events.GetEventByIdAsync(id, noIncludes: true);
            if (eventDb != null)
            {
                await _uow.Events.RemoveAsync(eventDb.Id);
            }

            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EventExists(int id)
        {
            return await _uow.Events.ExistsAsync(id);
        }

        public IEnumerable<IndexEventVM> CreateEventIndexVM(List<EventDTO>? events)
        {
            var eventVms = new List<IndexEventVM>();
            int numberOfEvents = events!.Count();
            for (int i = 0; i < numberOfEvents; i++)
            {
                var vm = new IndexEventVM()
                {
                    Id = events![i].Id,
                    LineNumber = i + 1,
                    Name = events[i].Name,
                    EventDateAndTime = events[i].EventDateAndTime,
                    Location = events[i].Location,
                    AdditionalInfo = events[i].AdditionalInfo
                };
                eventVms.Add(vm);
            }
            return eventVms;
        }

        public async Task<IActionResult> ListOfAttendees([FromRoute] int id)
        {
            var attendees = await _uow.Attendees.GetAllAttendeesOfEventOrderedByNameAsync(id)!;
            var numberOfAttendees = attendees?.Count ?? 0;
            var attendeeVms = new List<ListOfAttendeeVM>();

            if (numberOfAttendees > 0)
            {
                for (int i = 0; i < numberOfAttendees; i++)
                {
                    var vm = new ListOfAttendeeVM()
                    {
                        //LineNumber = i + 1,
                        Name = attendees?[i]!.AttendeeType == AttendeeType.Person ? attendees[i]!.SurAndGivenName : attendees?[i]!.CompanyName!,
                        PersonalIdentifier = attendees?[i]!.AttendeeType == AttendeeType.Person ? attendees[i]!.PersonalIdentifier : "",
                        RegistryCode = attendees?[i]!.AttendeeType == AttendeeType.Company ? attendees[i]!.RegistryCode : ""
                    };
                    attendeeVms.Add(vm);

                }
            }
            return View(attendeeVms);
        }
    }
}
