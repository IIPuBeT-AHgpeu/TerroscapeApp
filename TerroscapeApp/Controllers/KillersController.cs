using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TerroscapeApp.Database;

namespace TerroscapeApp.Controllers
{
    public class KillersController : Controller
    {
        private readonly TerroscapeStatsContext _context;

        public KillersController(TerroscapeStatsContext context)
        {
            _context = context;
        }

        // GET: Killers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Killers.ToListAsync());
        }

        // GET: Killers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var killer = await _context.Killers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (killer == null)
            {
                return NotFound();
            }

            return View(killer);
        }

        // GET: Killers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Killers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Strength,GameName")] Killer killer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(killer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(killer);
        }

        // GET: Killers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var killer = await _context.Killers.FindAsync(id);
            if (killer == null)
            {
                return NotFound();
            }
            return View(killer);
        }

        // POST: Killers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Strength,GameName")] Killer killer)
        {
            if (id != killer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(killer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KillerExists(killer.Id))
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
            return View(killer);
        }

        // GET: Killers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var killer = await _context.Killers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (killer == null)
            {
                return NotFound();
            }

            return View(killer);
        }

        // POST: Killers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var killer = await _context.Killers.FindAsync(id);
            if (killer != null)
            {
                _context.Killers.Remove(killer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KillerExists(int id)
        {
            return _context.Killers.Any(e => e.Id == id);
        }
    }
}
