using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

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

        // GET: Attendees/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Attendees/Create
        public IActionResult Create()
        {
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "Name");
            return View();
        }

        // POST: Attendees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttendeeType,SurName,GivenName,PersonalIdentifier,PersonAdditionalInfo,CompanyName,RegistryCode,NumberOfPeopleFromCompany,CompanyAdditionalInfo,PaymentMethodId,Id")] Attendee attendee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "Name", attendee.PaymentMethodId);
            return View(attendee);
        }

        // GET: Attendees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendee = await _context.Attendees.FindAsync(id);
            if (attendee == null)
            {
                return NotFound();
            }
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "Name", attendee.PaymentMethodId);
            return View(attendee);
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
