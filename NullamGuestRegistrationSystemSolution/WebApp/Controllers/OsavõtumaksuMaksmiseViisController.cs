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
    public class OsavõtumaksuMaksmiseViisController : Controller
    {
        private readonly AppDbContext _context;

        public OsavõtumaksuMaksmiseViisController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OsavõtumaksuMaksmiseViis
        public async Task<IActionResult> Index()
        {
            return View(await _context.OsavõtumaksuMaksmiseViisid.ToListAsync());
        }

        // GET: OsavõtumaksuMaksmiseViis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osavõtumaksuMaksmiseViis = await _context.OsavõtumaksuMaksmiseViisid
                .FirstOrDefaultAsync(m => m.Id == id);
            if (osavõtumaksuMaksmiseViis == null)
            {
                return NotFound();
            }

            return View(osavõtumaksuMaksmiseViis);
        }

        // GET: OsavõtumaksuMaksmiseViis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OsavõtumaksuMaksmiseViis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OsavõtumaksuMaksmiseViisiNimetus,Id")] OsavõtumaksuMaksmiseViis osavõtumaksuMaksmiseViis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(osavõtumaksuMaksmiseViis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(osavõtumaksuMaksmiseViis);
        }

        // GET: OsavõtumaksuMaksmiseViis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osavõtumaksuMaksmiseViis = await _context.OsavõtumaksuMaksmiseViisid.FindAsync(id);
            if (osavõtumaksuMaksmiseViis == null)
            {
                return NotFound();
            }
            return View(osavõtumaksuMaksmiseViis);
        }

        // POST: OsavõtumaksuMaksmiseViis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OsavõtumaksuMaksmiseViisiNimetus,Id")] OsavõtumaksuMaksmiseViis osavõtumaksuMaksmiseViis)
        {
            if (id != osavõtumaksuMaksmiseViis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(osavõtumaksuMaksmiseViis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OsavõtumaksuMaksmiseViisExists(osavõtumaksuMaksmiseViis.Id))
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
            return View(osavõtumaksuMaksmiseViis);
        }

        // GET: OsavõtumaksuMaksmiseViis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osavõtumaksuMaksmiseViis = await _context.OsavõtumaksuMaksmiseViisid
                .FirstOrDefaultAsync(m => m.Id == id);
            if (osavõtumaksuMaksmiseViis == null)
            {
                return NotFound();
            }

            return View(osavõtumaksuMaksmiseViis);
        }

        // POST: OsavõtumaksuMaksmiseViis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var osavõtumaksuMaksmiseViis = await _context.OsavõtumaksuMaksmiseViisid.FindAsync(id);
            if (osavõtumaksuMaksmiseViis != null)
            {
                _context.OsavõtumaksuMaksmiseViisid.Remove(osavõtumaksuMaksmiseViis);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OsavõtumaksuMaksmiseViisExists(int id)
        {
            return _context.OsavõtumaksuMaksmiseViisid.Any(e => e.Id == id);
        }
    }
}
