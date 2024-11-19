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
            var vm = new CreateEditAttendeeVM();
            var paymentMethods = await _uow.PaymentMethods.GetAllPaymentMehodsOrderedByNameAsync();
            vm.PaymentMethods = new SelectList(paymentMethods, nameof(PaymentMethod.Id), nameof(PaymentMethod.Name));
            return View(vm);
        }

        // POST: Attendees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditAttendeeVM vm, [FromRoute] int id)
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
            var vm = new CreateEditAttendeeVM();
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
        public async Task<IActionResult> Edit(int id, CreateEditAttendeeVM vm)
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
                    if (vm.AttendeeType == AttendeeType.Person)
                    {
                        attendeeDb.SurName = vm.SurName;
                        attendeeDb.GivenName = vm.GivenName;
                        attendeeDb.PersonalIdentifier = vm.PersonalIdentifier;
                        attendeeDb.PersonAdditionalInfo = vm.PersonAdditionalInfo;

                    }
                    else if (vm.AttendeeType == AttendeeType.Company)
                    {
                        attendeeDb.CompanyName = vm.CompanyName;
                        attendeeDb.RegistryCode = vm.RegistryCode;
                        attendeeDb.NumberOfPeopleFromCompany = vm.NumberOfPeopleFromCompany;
                        attendeeDb.CompanyAdditionalInfo = vm.CompanyAdditionalInfo;
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


        // POST: Attendees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendee = await _uow.Attendees.GetAttendeeByIdAsync(id, noIncludes: true);
            if (attendee != null)
            {

                await _uow.Attendees.RemoveAsync(attendee.Id, noIncludes: true);
            }

            await _uow.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        private async Task<bool> AttendeeExists(int id)
        {
            return await _uow.Attendees.ExistsAsync(id);
        }
    }
}
