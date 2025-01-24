﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TerroscapeApp.Database;
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
        public async Task<IActionResult> Create([Bind("Id,Date,MapId,KillerPlayerId,KillerId,KillerLevel,KillerBoostNum,FirstPlayerId,FirstAvatarId,FisrtState,SecondPlayerId,SecondAvatarId,SecondState,ThirdPlayerId,ThirdAvatarId,ThirdState,SurvivorBoostNum,HasPlans,DonePlan,GotKeys,DoneRadio,KillerWin,WinWay")] Round round)
        {
            if (round != null)
            {
                if (!round.HasPlans) round.DonePlan = null;
                if (round.Date == null) round.Date = DateTime.Now;
                if (round.SecondPlayerId == 0) round.SecondPlayerId = null;
                if (round.ThirdPlayerId == 0) round.ThirdPlayerId = null;
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
            ViewData["FirstAvatarId"] = new SelectList(_context.Avatars, "Id", "Id", round.FirstAvatarId);
            ViewData["FirstPlayerId"] = new SelectList(_context.Players, "Id", "Id", round.FirstPlayerId);
            ViewData["KillerId"] = new SelectList(_context.Killers, "Id", "Id", round.KillerId);
            ViewData["KillerPlayerId"] = new SelectList(_context.Players, "Id", "Id", round.KillerPlayerId);
            ViewData["MapId"] = new SelectList(_context.Maps, "Id", "Id", round.MapId);
            ViewData["SecondAvatarId"] = new SelectList(_context.Avatars, "Id", "Id", round.SecondAvatarId);
            ViewData["SecondPlayerId"] = new SelectList(_context.Players, "Id", "Id", round.SecondPlayerId);
            ViewData["ThirdAvatarId"] = new SelectList(_context.Avatars, "Id", "Id", round.ThirdAvatarId);
            ViewData["ThirdPlayerId"] = new SelectList(_context.Players, "Id", "Id", round.ThirdPlayerId);
            return View(round);
        }

        // POST: Rounds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MapId,KillerId,KillerPlayerId,KillerBoostNum,KillerWin,KillerLevel,SurvivorBoostNum,HasPlans,GotKeys,DoneRadio,DonePlan,FirstPlayerId,FirstAvatarId,FisrtState,SecondPlayerId,SecondAvatarId,SecondState,ThirdPlayerId,ThirdAvatarId,ThirdState,HowSurvivorsWin,HowKillerWin,Date")] Round round)
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
            ViewData["FirstAvatarId"] = new SelectList(_context.Avatars, "Id", "Id", round.FirstAvatarId);
            ViewData["FirstPlayerId"] = new SelectList(_context.Players, "Id", "Id", round.FirstPlayerId);
            ViewData["KillerId"] = new SelectList(_context.Killers, "Id", "Id", round.KillerId);
            ViewData["KillerPlayerId"] = new SelectList(_context.Players, "Id", "Id", round.KillerPlayerId);
            ViewData["MapId"] = new SelectList(_context.Maps, "Id", "Id", round.MapId);
            ViewData["SecondAvatarId"] = new SelectList(_context.Avatars, "Id", "Id", round.SecondAvatarId);
            ViewData["SecondPlayerId"] = new SelectList(_context.Players, "Id", "Id", round.SecondPlayerId);
            ViewData["ThirdAvatarId"] = new SelectList(_context.Avatars, "Id", "Id", round.ThirdAvatarId);
            ViewData["ThirdPlayerId"] = new SelectList(_context.Players, "Id", "Id", round.ThirdPlayerId);
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
