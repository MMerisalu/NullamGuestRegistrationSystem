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
using Microsoft.IdentityModel.Tokens;

namespace WebApp.Controllers
{
    public class EventsController : Controller
    {
        private readonly IAppUnitOfWork _uow;


        public EventsController(IAppUnitOfWork uow)
        {
            _uow = uow;

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
                return RedirectToAction("Index", "Home");
            }
            return View(vm);
        }


        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventDb = await _uow.Events.GetEventByIdAsync(id, noIncludes: true);

            var attendees = await _uow.Attendees.GetAllAttendeesOfEventOrderedByNameAsync(id)!;
            if (eventDb != null)
            {

                if (attendees.IsNullOrEmpty())
                {
                    await _uow.Events.RemoveAsync(eventDb.Id, noIncludes: true);
                }

                else
                {
                    foreach (var attendee in attendees!)
                    {
                        var numberOfEvents = await _uow.Attendees.NumberOfEventsForAttendeeAsync(attendee!.Id);

                        var eventAndAttendee = await _uow.EventsAndAttendes.GetEventAndAttendeeDTOAsync(eventDb.Id, attendee.Id);
                        if (eventAndAttendee != null)
                        {
                            await _uow.EventsAndAttendes.RemoveAsync(eventAndAttendee.Id!, noIncludes: true);
                            await _uow.SaveChangesAsync();
                        }
                        if (numberOfEvents == 1)
                        {
                            await _uow.Attendees.RemoveAsync(attendee.Id, noIncludes: true);
                            await _uow.SaveChangesAsync();
                        }
                    }

                    await _uow.Events.RemoveAsync(eventDb.Id, noIncludes: true);
                    await _uow.SaveChangesAsync();

                }
            }

            await _uow.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
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
                var vm = new IndexEventVM();
                vm.Id = events![i].Id;
                vm.LineNumber = i + 1;
                vm.Name = events[i].Name;
                vm.EventDateAndTime = events[i].EventDateAndTime;
                vm.Location = events[i].Location;
                vm.NumberOfAttendees = _uow.Events.NumberOfAttendeesPerEvent(events[i].Id);

                vm.AdditionalInfo = events[i].AdditionalInfo;
                eventVms.Add(vm);
            }
            return eventVms;
        }
        public async Task<IActionResult> ListOfAttendees([FromRoute] int id)
        {
            var attendees = await _uow.EventsAndAttendes.GetAllAttendeeDetailsDTOsByEventIdAsync(id, true, noIncludes: false);
            var numberOfAttendees = attendees?.Sum(x => x!.NumberOfPeople) ?? 0;
            var attendeeVms = new List<ListOfAttendeeVM>();

            if (numberOfAttendees > 0)
            {
                foreach (var attendee in attendees!)
                {
                    var vm = new ListOfAttendeeVM();
                    vm.Name = attendee!.Name!;
                    vm.Code = attendee!.Code!;
                    vm.Id = attendee!.Id;
                    vm.EventId = id;
                    vm.NumberOfPeople = attendee.NumberOfPeople;

                    attendeeVms.Add(vm);
                }
            }
            return View(attendeeVms);
        }

    }
}
