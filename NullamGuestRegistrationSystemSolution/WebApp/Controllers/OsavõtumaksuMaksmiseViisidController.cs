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
    public class OsavõtumaksuMaksmiseViisidController : Controller
    {
        private readonly AppDbContext _context;

        public OsavõtumaksuMaksmiseViisidController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OsavõtumaksuMaksmiseViisid
        public async Task<IActionResult> Index()
        {
            return View(await _context.OsavõtumaksuMaksmiseViisid.ToListAsync());
        }

        
        // GET: OsavõtumaksuMaksmiseViisid/Create
        public IActionResult Create()
        {
            var vm = new LisaMuudaMakseviisVM();
            return View(vm);
        }

        // POST: OsavõtumaksuMaksmiseViisid/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LisaMuudaMakseviisVM vm)
        {
            if (ModelState.IsValid)
            {
                var osavõtumaksuMaksmiseViis = new OsavõtumaksuMaksmiseViis();
                osavõtumaksuMaksmiseViis.OsavõtumaksuMaksmiseViisiNimetus = vm.OsavõtumaksuMaksmiseViisiNimetus;
                _context.Add(osavõtumaksuMaksmiseViis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: OsavõtumaksuMaksmiseViisid/Edit/5
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

        // POST: OsavõtumaksuMaksmiseViisid/Edit/5
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

        // GET: OsavõtumaksuMaksmiseViisid/Delete/5
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

        // POST: OsavõtumaksuMaksmiseViisid/Delete/5
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
