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
using App.Contracts.DAL;
using App.DAL.DTO;
using App.Enum;

namespace WebApp.Controllers
{
    public class AttendeesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public AttendeesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Attendees
        public async Task<IActionResult> Index()
        {

            return View(await _uow.Attendees.GetAllAttendeesOrderedByNameAsync());
        }


        // GET: Attendees/Create
        public async Task<IActionResult> Create()
        {
            var vm = new CreateAttendeeVM();
            var paymentMethods = await _uow.PaymentMethods.GetAllPaymentMehodsOrderedByNameAsync();
            vm.PaymentMethods = new SelectList(paymentMethods, nameof(PaymentMethod.Id), nameof(PaymentMethod.Name));
            return View(vm);
        }

        // POST: Attendees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAttendeeVM vm, [FromRoute] int id)
        {
            var attendee = new AttendeeDTO();

            if (!ModelState.IsValid)
            {
                var paymentMethods = await _uow.PaymentMethods.GetAllPaymentMehodsOrderedByNameAsync();
                vm.PaymentMethods = new SelectList(paymentMethods, nameof(PaymentMethod.Id), nameof(PaymentMethod.Name));
                return View(vm);
            }

            if (ModelState.IsValid)
            {
                attendee.AttendeeType = vm.AttendeeType;
                if (vm.AttendeeType == AttendeeType.Person)
                {
                    attendee.SurName = vm.SurName!;
                    attendee.GivenName = vm.GivenName!;
                    attendee.PersonalIdentifier = vm.PersonalIdentifier!;
                    attendee.PersonAdditionalInfo = vm.PersonAdditionalInfo;
                }
                else if (vm.AttendeeType == AttendeeType.Company)
                {
                    attendee.CompanyName = vm.CompanyName!;
                    attendee.RegistryCode = vm.RegistryCode!;
                    attendee.NumberOfPeopleFromCompany = vm.NumberOfPeopleFromCompany!.Value;
                    attendee.CompanyAdditionalInfo = vm.CompanyAdditionalInfo;
                }

                attendee.PaymentMethodId = vm.PaymentMethodId;
                var isRegistered = await _uow.Attendees.IsAttendeeAlreadyRegisteredAsync(vm.AttendeeType!.Value, vm.PersonalIdentifier, vm.CompanyName, vm.RegistryCode);
                if (isRegistered.Value == true)
                {
                    if (vm.AttendeeType == AttendeeType.Person)
                    {
                        ModelState.AddModelError("PersonalIdentifier", "Sisestatud isikukoodiga eraisik on juba registeeritud!");
                        var paymentMethods = await _uow.PaymentMethods.GetAllPaymentMehodsOrderedByNameAsync();
                        vm.PaymentMethods = new SelectList(paymentMethods, nameof(PaymentMethod.Id), nameof(PaymentMethod.Name));
                        return View(vm);
                    }
                    else if (vm.AttendeeType == AttendeeType.Company)
                    {
                        ModelState.AddModelError("CompanyName", "Sisestatud nime/registrikoodiga ettevõte on juba registeeritud!");
                        ModelState.AddModelError("RegistryCode", "Sisestatud nime/registrikoodiga ettevõte on juba registeeritud!");
                        var paymentMethods = await _uow.PaymentMethods.GetAllPaymentMehodsOrderedByNameAsync();
                        vm.PaymentMethods = new SelectList(paymentMethods, nameof(PaymentMethod.Id), nameof(PaymentMethod.Name));
                        return View(vm);
                    }
                }

                _uow.Attendees.Add(attendee);
                await _uow.SaveChangesAsync();
                int? attendeeId = null;

                if (vm.AttendeeType == AttendeeType.Person)
                {
                    attendeeId = _uow.Attendees.GetAttendeeId(AttendeeType.Person, vm.SurName, vm.GivenName);
                }
                else if (vm.AttendeeType == AttendeeType.Company)
                {
                    attendeeId = _uow.Attendees.GetAttendeeId(AttendeeType.Company, null, null, vm.CompanyName);
                }

                if (attendeeId != null)
                {
                    var eventAndAttendee = new EventAndAttendeeDTO()
                    {
                        EventId = id,
                        AttendeeId = attendeeId.Value,
                        NumberOfPeople = vm.AttendeeType.Value == AttendeeType.Company ? vm.NumberOfPeopleFromCompany.Value : 1

                    };
                    _uow.EventsAndAttendes.Add(eventAndAttendee);
                };
                await _uow.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            return View(vm);
        }


        // GET: Attendees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var vm = new EditAttendeeVM();
            var paymentMethods = await _uow.PaymentMethods.GetAllPaymentMehodsOrderedByNameAsync();
            vm.PaymentMethods = new SelectList(paymentMethods, nameof(PaymentMethod.Id), nameof(PaymentMethod.Name));

            if (id == null)
            {
                return NotFound();
            }

            var attendee = await _uow.Attendees.GetAttendeeByIdAsync(id.Value);
            if (attendee == null)
            {
                return NotFound();
            }

            vm.Id = attendee.Id;
            vm.AttendeeType = attendee.AttendeeType;
            if (vm.AttendeeType == AttendeeType.Person)
            {
                vm.SurName = attendee.SurName;
                vm.GivenName = attendee.GivenName;
                vm.PersonalIdentifier = attendee.PersonalIdentifier;
                vm.PersonAdditionalInfo = attendee.PersonAdditionalInfo;
            }
            else if (vm.AttendeeType == AttendeeType.Company)
            {
                vm.CompanyName = attendee.CompanyName;
                vm.RegistryCode = attendee.RegistryCode;
                vm.NumberOfPeopleFromCompany = attendee.NumberOfPeopleFromCompany;
                vm.CompanyAdditionalInfo = attendee.CompanyAdditionalInfo;
            }
            vm.PaymentMethodId = attendee.PaymentMethodId;

            return View(vm);
        }


        // POST: Attendees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditAttendeeVM vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }
            var attendeeDb = await _uow.Attendees.GetAttendeeByIdAsync(id);
            if (attendeeDb == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                var paymentMethods = await _uow.PaymentMethods.GetAllPaymentMehodsOrderedByNameAsync();
                vm.PaymentMethods = new SelectList(paymentMethods, nameof(PaymentMethod.Id), nameof(PaymentMethod.Name));
                return View(vm);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    vm.AttendeeType = attendeeDb.AttendeeType;
                    if (attendeeDb.AttendeeType!.Value == AttendeeType.Person)
                    {
                        attendeeDb.SurName = vm.SurName;
                        attendeeDb.GivenName = vm.GivenName;
                        attendeeDb.PersonalIdentifier = vm.PersonalIdentifier;
                        attendeeDb.PersonAdditionalInfo = vm.PersonAdditionalInfo;

                    }
                    else if (attendeeDb.AttendeeType.Value == AttendeeType.Company)
                    {
                        attendeeDb.CompanyName = vm.CompanyName;
                        attendeeDb.RegistryCode = vm.RegistryCode;
                        attendeeDb.NumberOfPeopleFromCompany = vm.NumberOfPeopleFromCompany!.Value;
                        attendeeDb.CompanyAdditionalInfo = vm.CompanyAdditionalInfo;
                    }

                    attendeeDb.PaymentMethodId = vm.PaymentMethodId;
                    var isRegistered = await _uow.Attendees.IsAttendeeAlreadyRegisteredAsync(attendeeDb!.AttendeeType!.Value, attendeeDb.PersonalIdentifier, attendeeDb.CompanyName, attendeeDb.RegistryCode);
                    if (isRegistered.Value == true)
                    {
    
                        if (attendeeDb.AttendeeType.Value == AttendeeType.Person)
                       {
                            ModelState.AddModelError("PersonalIdentifier", "Sisestatud isikukoodiga eraisik on juba registeeritud!");
                            var paymentMethods = await _uow.PaymentMethods.GetAllPaymentMehodsOrderedByNameAsync();
                            vm.PaymentMethods = new SelectList(paymentMethods, nameof(PaymentMethod.Id), nameof(PaymentMethod.Name));
                            return View(vm);
                        }
                        else if (attendeeDb.AttendeeType.Value == AttendeeType.Company)
                        {
                            ModelState.AddModelError("CompanyName", "Sisestatud nime/registrikoodiga ettevõte on juba registeeritud!");
                            ModelState.AddModelError("RegistryCode", "Sisestatud nime/registrikoodiga ettevõte on juba registeeritud!");
                            var paymentMethods = await _uow.PaymentMethods.GetAllPaymentMehodsOrderedByNameAsync();
                            vm.PaymentMethods = new SelectList(paymentMethods, nameof(PaymentMethod.Id), nameof(PaymentMethod.Name));
                            return View(vm);
                        }
                    }


                    if (attendeeDb.AttendeeType == AttendeeType.Person)
                    {
                        attendeeDb.SurName = vm.SurName;
                        attendeeDb.GivenName = vm.GivenName;
                        attendeeDb.PersonalIdentifier = vm.PersonalIdentifier;
                        attendeeDb.PersonAdditionalInfo = vm.PersonAdditionalInfo;
                        attendeeDb.PaymentMethodId = vm.PaymentMethodId;
                    }
                    else if (attendeeDb.AttendeeType == AttendeeType.Company)
                    {
                        attendeeDb.CompanyName = vm.CompanyName;
                        attendeeDb.RegistryCode = vm.RegistryCode;
                        attendeeDb.CompanyAdditionalInfo = vm.CompanyAdditionalInfo;
                        attendeeDb.PaymentMethodId = vm.PaymentMethodId;
                    }

                    _uow.Attendees.Update(attendeeDb);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.Attendees.ExistsAsync(id))
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


        // POST: Events/1/Attendees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int eventId)
        {
            var attendee = await _uow.Attendees.GetAttendeeByIdAsync(id, noIncludes: true);
            
            var eventDb = await _uow.Events.GetEventByIdAsync(eventId);
            var eventAndAttendee = await _uow.EventsAndAttendes.GetEventAndAttendeeDTOAsync(eventDb!.Id, id);
            await _uow.EventsAndAttendes.RemoveAsync(eventAndAttendee!.Id);
            await _uow.SaveChangesAsync();
            var hasEvents = await _uow.Attendees.IsAttendeeAttendingAnyEventsAsync(attendee!.Id);
            if (!hasEvents && attendee != null)
            {
                await _uow.Attendees.RemoveAsync(attendee.Id, noIncludes: true);
            }
                
            await _uow.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> AddAttendeeToAnotherEvent([FromRoute] int id)
        {
           
            var attendeeDb = await _uow.Attendees.GetAttendeeByIdAsync(id, noIncludes: true);
            if (attendeeDb == null)
            {
                return NotFound();
            }

            var vm = new AddAttendeeToAnotherEventVM();
            vm.AttendeeType = attendeeDb.AttendeeType!.Value;
            if (vm.AttendeeType.Value == AttendeeType.Company)
                vm.NumberOfPeopleFromCompany = attendeeDb.NumberOfPeopleFromCompany!.Value;
            vm.Events = new SelectList(await _uow.Events.GetAllFutureEventsrderedByTimeAndNameAsync(attendeeDb.Id), nameof(EventDTO.Id), nameof(EventDTO.EventDateTimeAndName));
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAttendeeToAnotherEvent(AddAttendeeToAnotherEventVM vm, [FromRoute] int id)
        {
            var attendeeDb = await _uow.Attendees.GetAttendeeByIdAsync(id);
            if (attendeeDb == null)
            {
                return NotFound();
            }

            if (attendeeDb.AttendeeType == AttendeeType.Company &&
                vm.NumberOfPeopleFromCompany != attendeeDb.NumberOfPeopleFromCompany)
            {
                attendeeDb.NumberOfPeopleFromCompany = vm.NumberOfPeopleFromCompany!.Value;
            }

            var eventAndAttendee = new EventAndAttendeeDTO()
            {
                AttendeeId = attendeeDb.Id,
                EventId = vm.EventId,
                NumberOfPeople = attendeeDb.AttendeeType.Value == AttendeeType.Company ? vm.NumberOfPeopleFromCompany.Value : 1
            };
             _uow.EventsAndAttendes.Add(eventAndAttendee);
            await _uow.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
          }
        private async Task<bool> AttendeeExists(int id)
        {
            return await _uow.Attendees.ExistsAsync(id);
        }
    }
}
