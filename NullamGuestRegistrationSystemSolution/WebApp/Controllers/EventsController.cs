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

namespace WebApp.Controllers
{
    public class EventsController : Controller
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var events = await CreateEventIndexVM();
            
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
            var newEvent = new Event();
            newEvent.Name = vm.Name;
            newEvent.EventDateAndTime = DateTime.Parse(vm.EventDateAndTime);
            newEvent.Location = vm.Location;
            newEvent.AdditionalInfo = vm.AdditionalInfo;

            if (ModelState.IsValid)
            {
                _context.Add(newEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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

            var eventdb = await _context.Events.FindAsync(id);
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
            var eventDb = await _context.Events.FindAsync(vm.Id);
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
                    _context.Update(eventDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(eventDb.Id))
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

            var eventDb = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var eventDb = await _context.Events.FindAsync(id);
            if (eventDb != null)
            {
                _context.Events.Remove(eventDb);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }

        private async Task<List<IndexEventVM>> CreateEventIndexVM()
        {
            var events = await _context.Events.ToListAsync();
            var eventVms = new List<IndexEventVM>();
            int numberOfEvents = events.Count;
            for (int i = 0; i < numberOfEvents; i++)
            {
                var vm = new IndexEventVM()
                {
                    Id = events[i].Id,
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
    }
       
    
}
