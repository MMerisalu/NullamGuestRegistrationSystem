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
    public class OsavõtjaController : Controller
    {
        private readonly AppDbContext _context;

        public OsavõtjaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Osavõtja
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Osavõtjad.Include(o => o.OsavotumaksuMaksmiseViis);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Osavõtja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osavõtja = await _context.Osavõtjad
                .Include(o => o.OsavotumaksuMaksmiseViis)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (osavõtja == null)
            {
                return NotFound();
            }

            return View(osavõtja);
        }

        // GET: Osavõtja/Create
        public IActionResult Create()
        {
            ViewData["OsavotumaksuMaksmiseViisId"] = new SelectList(_context.OsavõtumaksuMaksmiseViisid, "Id", "OsavõtumaksuMaksmiseViisiNimetus");
            return View();
        }

        // POST: Osavõtja/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OsavõtjaTüüp,Eesnimi,Perekonnanimi,Isikukood,EraisikuLisainfo,EttevõtteJuriidilineNimi,EttevõtteRegistrikood,EttevõttestTulevateOsavõtjateArv,OsavotumaksuMaksmiseViisId,Id")] Osavõtja osavõtja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(osavõtja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OsavotumaksuMaksmiseViisId"] = new SelectList(_context.OsavõtumaksuMaksmiseViisid, "Id", "OsavõtumaksuMaksmiseViisiNimetus", osavõtja.OsavotumaksuMaksmiseViisId);
            return View(osavõtja);
        }

        // GET: Osavõtja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osavõtja = await _context.Osavõtjad.FindAsync(id);
            if (osavõtja == null)
            {
                return NotFound();
            }
            ViewData["OsavotumaksuMaksmiseViisId"] = new SelectList(_context.OsavõtumaksuMaksmiseViisid, "Id", "OsavõtumaksuMaksmiseViisiNimetus", osavõtja.OsavotumaksuMaksmiseViisId);
            return View(osavõtja);
        }

        // POST: Osavõtja/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OsavõtjaTüüp,Eesnimi,Perekonnanimi,Isikukood,EraisikuLisainfo,EttevõtteJuriidilineNimi,EttevõtteRegistrikood,EttevõttestTulevateOsavõtjateArv,OsavotumaksuMaksmiseViisId,Id")] Osavõtja osavõtja)
        {
            if (id != osavõtja.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(osavõtja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OsavõtjaExists(osavõtja.Id))
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
            ViewData["OsavotumaksuMaksmiseViisId"] = new SelectList(_context.OsavõtumaksuMaksmiseViisid, "Id", "OsavõtumaksuMaksmiseViisiNimetus", osavõtja.OsavotumaksuMaksmiseViisId);
            return View(osavõtja);
        }

        // GET: Osavõtja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osavõtja = await _context.Osavõtjad
                .Include(o => o.OsavotumaksuMaksmiseViis)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (osavõtja == null)
            {
                return NotFound();
            }

            return View(osavõtja);
        }

        // POST: Osavõtja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var osavõtja = await _context.Osavõtjad.FindAsync(id);
            if (osavõtja != null)
            {
                _context.Osavõtjad.Remove(osavõtja);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OsavõtjaExists(int id)
        {
            return _context.Osavõtjad.Any(e => e.Id == id);
        }
    }
}
