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

namespace WebApp.Controllers
{
    public class AttendeesController : Controller
    {
        private readonly AppDbContext _context;

        public AttendeesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Attendees
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Attendees.Include(a => a.PaymentMethod);
            return View(await appDbContext.ToListAsync());
        }


        // GET: Attendees/Create
        public async Task<IActionResult> Create()
        {
            var vm = new CreateEditAttendeeVM();
            var paymentMethods = await _context.PaymentMethods.ToListAsync();
            vm.PaymentMethods = new SelectList(paymentMethods, nameof(PaymentMethod.Id), nameof(PaymentMethod.Name));
            return View(vm);
        }

        // POST: Attendees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditAttendeeVM vm)
        {
            var attendee = new Attendee();

            if (!ModelState.IsValid)
            {
                var paymentMethods = await _context.PaymentMethods.ToListAsync();
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

                _context.Add(attendee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Attendees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var vm = new CreateEditAttendeeVM();
            var paymentMethods = await _context.PaymentMethods.ToListAsync();
            vm.PaymentMethods = new SelectList(paymentMethods, nameof(PaymentMethod.Id), nameof(PaymentMethod.Name));

            if (id == null)
            {
                return NotFound();
            }

            var attendee = await _context.Attendees.FindAsync(id);
            if (attendee == null)
            {
                return NotFound();
            }

            vm.Id = attendee.Id;
            vm.AttendeeType = attendee.AttendeeType;
            if(vm.AttendeeType == AttendeeType.Person)
            {
                vm.SurName = attendee.SurName;
                vm.GivenName = attendee.GivenName;
                vm.PersonalIdentifier = attendee.PersonalIdentifier;
                vm.PersonAdditionalInfo = attendee.PersonAdditionalInfo;
            }
            else if(vm.AttendeeType == AttendeeType.Company)
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
        public async Task<IActionResult> Edit(int id, [Bind("AttendeeType,SurName,GivenName,PersonalIdentifier,PersonAdditionalInfo,CompanyName,RegistryCode,NumberOfPeopleFromCompany,CompanyAdditionalInfo,PaymentMethodId,Id")] Attendee attendee)
        {
            if (id != attendee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendeeExists(attendee.Id))
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
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "Name", attendee.PaymentMethodId);
            return View(attendee);
        }

        // GET: Attendees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendee = await _context.Attendees
                .Include(a => a.PaymentMethod)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendee == null)
            {
                return NotFound();
            }

            return View(attendee);
        }

        // POST: Attendees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendee = await _context.Attendees.FindAsync(id);
            if (attendee != null)
            {
                _context.Attendees.Remove(attendee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendeeExists(int id)
        {
            return _context.Attendees.Any(e => e.Id == id);
        }
    }
}
