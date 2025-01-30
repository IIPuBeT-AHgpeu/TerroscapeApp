using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TerroscapeApp.Database;
using TerroscapeApp.Models.ViewModels;
using TerroscapeApp.Service;

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
            var terroscapeStatsContext = _context.Rounds
                .Include(r => r.FirstAvatar)
                .Include(r => r.FirstPlayer)
                .Include(r => r.Killer)
                .Include(r => r.KillerPlayer)
                .Include(r => r.Map)
                .Include(r => r.SecondAvatar)
                .Include(r => r.SecondPlayer)
                .Include(r => r.ThirdAvatar)
                .Include(r => r.ThirdPlayer);

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
                .Include(r => r.FirstAvatar)
                .Include(r => r.FirstPlayer)
                .Include(r => r.Killer)
                .Include(r => r.KillerPlayer)
                .Include(r => r.Map)
                .Include(r => r.SecondAvatar)
                .Include(r => r.SecondPlayer)
                .Include(r => r.ThirdAvatar)
                .Include(r => r.ThirdPlayer)
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
            ViewData["FirstAvatarList"] = new SelectList(_context.Avatars, "Id", "Name");
            ViewData["FirstPlayerList"] = new SelectList(_context.Players, "Id", "Name");

            ViewData["KillersList"] = new SelectList(_context.Killers, "Id", "Name");
            ViewData["KillerPlayerList"] = new SelectList(_context.Players, "Id", "Name");

            ViewData["MapList"] = new SelectList(_context.Maps, "Id", "Name");

            var players = (from p in _context.Players select new { Id = p.Id, Name = p.Name }).ToList();
            players.Add(new { Id = 0, Name = "Нет игрока" });

            ViewData["SecondAvatarList"] = new SelectList(_context.Avatars, "Id", "Name");
            ViewData["SecondPlayerList"] = new SelectList(players, "Id", "Name");

            ViewData["ThirdAvatarList"] = new SelectList(_context.Avatars, "Id", "Name");
            ViewData["ThirdPlayerList"] = new SelectList(players, "Id", "Name");

            return View();
        }

        // POST: Rounds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,MapId,KillerPlayerId,KillerId,KillerLevel,KillerBoostNum,FirstPlayerId,FirstAvatarId,FisrtState,SecondPlayerId,SecondAvatarId,SecondState,ThirdPlayerId,ThirdAvatarId,ThirdState,SurvivorBoostNum,HasPlans,DonePlan,GotKeys,DoneRadio,KillerWin,WinWay")] AddRoundViewModel roundFromView)
        {
            if (roundFromView != null)
            {
                Round round = (Round)roundFromView;
                _context.Add(round);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["FirstAvatarList"] = new SelectList(_context.Avatars, "Id", "Name");
            ViewData["FirstPlayerList"] = new SelectList(_context.Players, "Id", "Name");

            ViewData["KillersList"] = new SelectList(_context.Killers, "Id", "Name");
            ViewData["KillerPlayerList"] = new SelectList(_context.Players, "Id", "Name");

            ViewData["MapList"] = new SelectList(_context.Maps, "Id", "Name");

            var players = (from p in _context.Players select new { Id = p.Id, Name = p.Name }).ToList();
            players.Add(new { Id = 0, Name = "Нет игрока" });

            ViewData["SecondAvatarList"] = new SelectList(_context.Avatars, "Id", "Name");
            ViewData["SecondPlayerList"] = new SelectList(players, "Id", "Name");

            ViewData["ThirdAvatarList"] = new SelectList(_context.Avatars, "Id", "Name");
            ViewData["ThirdPlayerList"] = new SelectList(players, "Id", "Name");

            return View(roundFromView);
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

            ViewData["FirstAvatarList"] = new SelectList(_context.Avatars, "Id", "Name");
            ViewData["FirstPlayerList"] = new SelectList(_context.Players, "Id", "Name");

            ViewData["KillersList"] = new SelectList(_context.Killers, "Id", "Name");
            ViewData["KillerPlayerList"] = new SelectList(_context.Players, "Id", "Name");

            ViewData["MapList"] = new SelectList(_context.Maps, "Id", "Name");

            var players = (from p in _context.Players select new { Id = p.Id, Name = p.Name }).ToList();
            players.Add(new { Id = 0, Name = "Нет игрока" });

            ViewData["SecondAvatarList"] = new SelectList(_context.Avatars, "Id", "Name");
            ViewData["SecondPlayerList"] = new SelectList(players, "Id", "Name");

            ViewData["ThirdAvatarList"] = new SelectList(_context.Avatars, "Id", "Name");
            ViewData["ThirdPlayerList"] = new SelectList(players, "Id", "Name");

            return View((EditRoundViewModel)round);
        }

        // POST: Rounds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,MapId,KillerPlayerId,KillerId,KillerLevel,KillerBoostNum,FirstPlayerId,FirstAvatarId,FisrtState,SecondPlayerId,SecondAvatarId,SecondState,ThirdPlayerId,ThirdAvatarId,ThirdState,SurvivorBoostNum,HasPlans,DonePlan,GotKeys,DoneRadio,KillerWin,WinWay")] EditRoundViewModel roundFromView)
        {
            if (roundFromView != null) 
            {
                if (id != roundFromView.Id)
                {
                    return NotFound();
                }

                var round = (Round)roundFromView;

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
            else
            {
                ViewData["FirstAvatarList"] = new SelectList(_context.Avatars, "Id", "Name");
                ViewData["FirstPlayerList"] = new SelectList(_context.Players, "Id", "Name");

                ViewData["KillersList"] = new SelectList(_context.Killers, "Id", "Name");
                ViewData["KillerPlayerList"] = new SelectList(_context.Players, "Id", "Name");

                ViewData["MapList"] = new SelectList(_context.Maps, "Id", "Name");

                var players = (from p in _context.Players select new { Id = p.Id, Name = p.Name }).ToList();
                players.Add(new { Id = 0, Name = "Нет игрока" });

                ViewData["SecondAvatarList"] = new SelectList(_context.Avatars, "Id", "Name");
                ViewData["SecondPlayerList"] = new SelectList(players, "Id", "Name");

                ViewData["ThirdAvatarList"] = new SelectList(_context.Avatars, "Id", "Name");
                ViewData["ThirdPlayerList"] = new SelectList(players, "Id", "Name");

                return View(roundFromView);
            }          
        }

        // GET: Rounds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var round = await _context.Rounds
                .Include(r => r.FirstAvatar)
                .Include(r => r.FirstPlayer)
                .Include(r => r.Killer)
                .Include(r => r.KillerPlayer)
                .Include(r => r.Map)
                .Include(r => r.SecondAvatar)
                .Include(r => r.SecondPlayer)
                .Include(r => r.ThirdAvatar)
                .Include(r => r.ThirdPlayer)
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
