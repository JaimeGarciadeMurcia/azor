using Azor.Web.Models.Entities;
using Azor.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Azor.Web.Controllers;

public class SeasonController : Controller
{
    private readonly ISeasonStorage _seasonStorage;
    private readonly IBattleReplayStorage _replayStorage;

    public SeasonController(ISeasonStorage seasonStorage, IBattleReplayStorage replayStorage)
    {
        _seasonStorage = seasonStorage;
        _replayStorage = replayStorage;
    }

    public IActionResult Index()
    {
        var seasons = _seasonStorage.GetAll();
        return View(seasons);
    }

    public IActionResult Create()
    {
        var season = new Season
        {
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddDays(1)
        };
        return View(season);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Season season)
    {
        if (season.EndDate < season.StartDate)
            ModelState.AddModelError("EndDate", "La fecha de fin no puede ser anterior a la fecha de inicio.");

        if (ModelState.IsValid)
        {
            _seasonStorage.Add(season);
            return RedirectToAction(nameof(Index));
        }

        return View(season);
    }

    public IActionResult Edit(int id)
    {
        var season = _seasonStorage.GetById(id);
        if (season == null) return NotFound();
        return View(season);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Season season)
    {
        if (ModelState.IsValid)
        {
            _seasonStorage.Update(season);
            return RedirectToAction(nameof(Index));
        }

        return View(season);
    }

    public IActionResult Delete(int id)
    {
        var season = _seasonStorage.GetById(id);
        if (season == null) return NotFound();
        return View(season);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        _seasonStorage.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    // Estadísticas de asistencia para una temporada
    public IActionResult AttendanceStats(int id)
    {
        var season = _seasonStorage.GetById(id);
        if (season == null) return NotFound();

        const string targetClan = "AZOR";
        
        int clanBattleDays = GetClanBattleDays(season.StartDate, season.EndDate);
        int requiredMatches = clanBattleDays;

        var replays = _replayStorage.GetAll();
        var matchesInSeason = replays
            .Where(r => r.Metadata != null && r.Metadata.Timestamp >= season.StartDate && r.Metadata.Timestamp <= season.EndDate)
            .ToList();

        // Diccionario de partidas por jugador (db_id)
        var playerMatches = new Dictionary<long, List<DateTime>>();
        foreach (var replay in matchesInSeason)
        {
            foreach (var vehicle in replay.Vehicles)
            {
                if (vehicle.PlayerBattle?.Clan == targetClan) // ⬅️ FILTRAR AQUÍ MISMO
                {
                    var dbId = vehicle.PlayerBattle.DbId;
                    if (!playerMatches.ContainsKey(dbId))
                        playerMatches[dbId] = new List<DateTime>();
                    playerMatches[dbId].Add(replay.Metadata.Timestamp);
                }
            }
        }

        var stats = new List<PlayerAttendance>();
        foreach (var kvp in playerMatches)
        {
            var anyVehicle = replays.SelectMany(r => r.Vehicles).FirstOrDefault(v => v.PlayerBattle?.DbId == kvp.Key);
            if (anyVehicle?.PlayerBattle == null) continue;

            stats.Add(new PlayerAttendance
            {
                PlayerDbId = kvp.Key,
                PlayerName = anyVehicle.PlayerBattle.Name,
                Clan = anyVehicle.PlayerBattle.Clan,
                MatchesPlayed = kvp.Value.Count,
                RequiredMatches = requiredMatches,
                MeetsRequirement = kvp.Value.Count >= requiredMatches
            });
        }

        ViewBag.Season = season;
        ViewBag.TargetClan = targetClan;
        return View(stats.OrderByDescending(s => s.MatchesPlayed).ToList());
    }
    
    private static int GetClanBattleDays(DateTime start, DateTime end)
    {
        int count = 0;
        for (var date = start.Date; date <= end.Date; date = date.AddDays(1))
        {
            if (date.DayOfWeek == DayOfWeek.Wednesday ||
                date.DayOfWeek == DayOfWeek.Thursday ||
                date.DayOfWeek == DayOfWeek.Saturday ||
                date.DayOfWeek == DayOfWeek.Sunday)
            {
                count++;
            }
        }
        return count;
    }
}