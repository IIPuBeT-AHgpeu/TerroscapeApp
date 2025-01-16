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
    public class RoundsController : Controller
    {
        private readonly TerroscapeStatsContext _context;

        public RoundsController(TerroscapeStatsContext context)
        {
            _context = context;
        }

        // GET: Rounds
        public async Task<IActionResult> Index()
        {
            var terroscapeStatsContext = _context.Rounds.Include(r => r.FirstSurvivorNavigation).Include(r => r.Killer).Include(r => r.KillerPlayer).Include(r => r.Map).Include(r => r.SecondSurvivorNavigation).Include(r => r.ThirdSurvivorNavigation);
            return View(await terroscapeStatsContext.ToListAsync());
        }

        // GET: Rounds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var round = await _context.Rounds
                .Include(r => r.FirstSurvivorNavigation)
                .Include(r => r.Killer)
                .Include(r => r.KillerPlayer)
                .Include(r => r.Map)
                .Include(r => r.SecondSurvivorNavigation)
                .Include(r => r.ThirdSurvivorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (round == null)
            {
                return NotFound();
            }

            return View(round);
        }

        // GET: Rounds/Create
        public IActionResult Create()
        {
            ViewData["FirstSurvivor"] = new SelectList(_context.Survivors, "Id", "Id");
            ViewData["KillerId"] = new SelectList(_context.Killers, "Id", "Id");
            ViewData["KillerPlayerId"] = new SelectList(_context.Players, "Id", "Id");
            ViewData["MapId"] = new SelectList(_context.Maps, "Id", "Id");
            ViewData["SecondSurvivor"] = new SelectList(_context.Survivors, "Id", "Id");
            ViewData["ThirdSurvivor"] = new SelectList(_context.Survivors, "Id", "Id");
            return View();
        }

        // POST: Rounds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MapId,KillerId,KillerPlayerId,KillerBoostNum,KillerWin,KillerLevel,FirstSurvivor,SecondSurvivor,ThirdSurvivor,SurvivorBoostNum,HasPlans,GotKeys,DoneRadio,DonePlan,HowSurvivorsWin,HowKillerWin")] Round round)
        {
            if (ModelState.IsValid)
            {
                _context.Add(round);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FirstSurvivor"] = new SelectList(_context.Survivors, "Id", "Id", round.FirstSurvivor);
            ViewData["KillerId"] = new SelectList(_context.Killers, "Id", "Id", round.KillerId);
            ViewData["KillerPlayerId"] = new SelectList(_context.Players, "Id", "Id", round.KillerPlayerId);
            ViewData["MapId"] = new SelectList(_context.Maps, "Id", "Id", round.MapId);
            ViewData["SecondSurvivor"] = new SelectList(_context.Survivors, "Id", "Id", round.SecondSurvivor);
            ViewData["ThirdSurvivor"] = new SelectList(_context.Survivors, "Id", "Id", round.ThirdSurvivor);
            return View(round);
        }

        // GET: Rounds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var round = await _context.Rounds.FindAsync(id);
            if (round == null)
            {
                return NotFound();
            }
            ViewData["FirstSurvivor"] = new SelectList(_context.Survivors, "Id", "Id", round.FirstSurvivor);
            ViewData["KillerId"] = new SelectList(_context.Killers, "Id", "Id", round.KillerId);
            ViewData["KillerPlayerId"] = new SelectList(_context.Players, "Id", "Id", round.KillerPlayerId);
            ViewData["MapId"] = new SelectList(_context.Maps, "Id", "Id", round.MapId);
            ViewData["SecondSurvivor"] = new SelectList(_context.Survivors, "Id", "Id", round.SecondSurvivor);
            ViewData["ThirdSurvivor"] = new SelectList(_context.Survivors, "Id", "Id", round.ThirdSurvivor);
            return View(round);
        }

        // POST: Rounds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MapId,KillerId,KillerPlayerId,KillerBoostNum,KillerWin,KillerLevel,FirstSurvivor,SecondSurvivor,ThirdSurvivor,SurvivorBoostNum,HasPlans,GotKeys,DoneRadio,DonePlan,HowSurvivorsWin,HowKillerWin")] Round round)
        {
            if (id != round.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(round);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoundExists(round.Id))
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
            ViewData["FirstSurvivor"] = new SelectList(_context.Survivors, "Id", "Id", round.FirstSurvivor);
            ViewData["KillerId"] = new SelectList(_context.Killers, "Id", "Id", round.KillerId);
            ViewData["KillerPlayerId"] = new SelectList(_context.Players, "Id", "Id", round.KillerPlayerId);
            ViewData["MapId"] = new SelectList(_context.Maps, "Id", "Id", round.MapId);
            ViewData["SecondSurvivor"] = new SelectList(_context.Survivors, "Id", "Id", round.SecondSurvivor);
            ViewData["ThirdSurvivor"] = new SelectList(_context.Survivors, "Id", "Id", round.ThirdSurvivor);
            return View(round);
        }

        // GET: Rounds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var round = await _context.Rounds
                .Include(r => r.FirstSurvivorNavigation)
                .Include(r => r.Killer)
                .Include(r => r.KillerPlayer)
                .Include(r => r.Map)
                .Include(r => r.SecondSurvivorNavigation)
                .Include(r => r.ThirdSurvivorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (round == null)
            {
                return NotFound();
            }

            return View(round);
        }

        // POST: Rounds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var round = await _context.Rounds.FindAsync(id);
            if (round != null)
            {
                _context.Rounds.Remove(round);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoundExists(int id)
        {
            return _context.Rounds.Any(e => e.Id == id);
        }
    }
}
