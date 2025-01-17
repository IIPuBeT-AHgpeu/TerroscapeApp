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
    public class SurvivorsController : Controller
    {
        private readonly TerroscapeStatsContext _context;

        public SurvivorsController(TerroscapeStatsContext context)
        {
            _context = context;
        }

        // GET: Survivors
        public async Task<IActionResult> Index()
        {
            var terroscapeStatsContext = _context.Survivors.Include(s => s.Avatar).Include(s => s.Player);
            return View(await terroscapeStatsContext.ToListAsync());
        }

        // GET: Survivors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survivor = await _context.Survivors
                .Include(s => s.Avatar)
                .Include(s => s.Player)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (survivor == null)
            {
                return NotFound();
            }

            return View(survivor);
        }

        // GET: Survivors/Create
        public IActionResult Create()
        {
            var players = from p in _context.Players select new { Id = p.Id, Name = p.Name };
            var pl = players.ToList(); pl.Add(new { Id = 0, Name = "BOT" });

            ViewData["Avatars"] = new SelectList(_context.Avatars, "Id", "Name");
            ViewData["Players"] = new SelectList(pl, "Id", "Name");
            return View();
        }

        // POST: Survivors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlayerId,AvatarId,State")] Survivor survivor)
        {
            if (survivor != null)
            {
                var avatar = _context.Avatars.FirstOrDefault(a => a.Id == survivor.AvatarId);
                if (avatar != null)
                {
                    if (survivor.PlayerId == 0) survivor.PlayerId = null;

                    _context.Add(survivor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            var players = from p in _context.Players select new { Id = p.Id, Name = p.Name };
            var pl = players.ToList(); pl.Add(new { Id = 0, Name = "BOT" });

            ViewData["Avatars"] = new SelectList(_context.Avatars, "Id", "Name");
            ViewData["Players"] = new SelectList(pl, "Id", "Name");
            return View(survivor);           
        }

        // GET: Survivors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survivor = await _context.Survivors.FindAsync(id);
            if (survivor == null)
            {
                return NotFound();
            }

            var players = from p in _context.Players select new { Id = p.Id, Name = p.Name };
            var pl = players.ToList(); pl.Add(new { Id = 0, Name = "BOT" });

            ViewData["Avatars"] = new SelectList(_context.Avatars, "Id", "Name", survivor.AvatarId);
            if (survivor.PlayerId != null && survivor.PlayerId > 0) ViewData["Players"] = new SelectList(pl, "Id", "Name", survivor.PlayerId);
            else ViewData["Players"] = new SelectList(pl, "Id", "Name", 0);

            return View(survivor);
        }

        // POST: Survivors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlayerId,AvatarId,State")] Survivor survivor)
        {
            if (id != survivor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (survivor.PlayerId == 0) survivor.PlayerId = null;

                    _context.Update(survivor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurvivorExists(survivor.Id))
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

            var players = from p in _context.Players select new { Id = p.Id, Name = p.Name };
            var pl = players.ToList(); pl.Add(new { Id = 0, Name = "BOT" });

            ViewData["Avatars"] = new SelectList(_context.Avatars, "Id", "Name", survivor.AvatarId);
            if (survivor.PlayerId != null && survivor.PlayerId > 0) ViewData["Players"] = new SelectList(pl, "Id", "Name", survivor.PlayerId);
            else ViewData["Players"] = new SelectList(pl, "Id", "Name", 0);

            return View(survivor);
        }

        // GET: Survivors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survivor = await _context.Survivors
                .Include(s => s.Avatar)
                .Include(s => s.Player)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (survivor == null)
            {
                return NotFound();
            }

            return View(survivor);
        }

        // POST: Survivors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var survivor = await _context.Survivors.FindAsync(id);
            if (survivor != null)
            {
                _context.Survivors.Remove(survivor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurvivorExists(int id)
        {
            return _context.Survivors.Any(e => e.Id == id);
        }
    }
}
