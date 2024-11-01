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
    public class ÜritusedController : Controller
    {
        private readonly AppDbContext _context;

        public ÜritusedController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Üritused
        public async Task<IActionResult> Index()
        {
            return View(await _context.Üritused.ToListAsync());
        }

        // GET: Üritused/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var üritus = await _context.Üritused
                .FirstOrDefaultAsync(m => m.Id == id);
            if (üritus == null)
            {
                return NotFound();
            }

            return View(üritus);
        }

        // GET: Üritused/Create
        public IActionResult Create()
        {
            var vm = new LisaMuudaÜritusVM();
            return View(vm);
        }

        // POST: Üritused/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ÜrituseNimi,Toimumisaeg,Koht,Lisainfo,OsavõtjateArv,Id")] Üritus üritus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(üritus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(üritus);
        }

        // GET: Üritused/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var üritus = await _context.Üritused.FindAsync(id);
            if (üritus == null)
            {
                return NotFound();
            }
            return View(üritus);
        }

        // POST: Üritused/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ÜrituseNimi,Toimumisaeg,Koht,Lisainfo,OsavõtjateArv,Id")] Üritus üritus)
        {
            if (id != üritus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(üritus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ÜritusExists(üritus.Id))
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
            return View(üritus);
        }

        // GET: Üritused/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var üritus = await _context.Üritused
                .FirstOrDefaultAsync(m => m.Id == id);
            if (üritus == null)
            {
                return NotFound();
            }

            return View(üritus);
        }

        // POST: Üritused/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var üritus = await _context.Üritused.FindAsync(id);
            if (üritus != null)
            {
                _context.Üritused.Remove(üritus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ÜritusExists(int id)
        {
            return _context.Üritused.Any(e => e.Id == id);
        }
    }
}
