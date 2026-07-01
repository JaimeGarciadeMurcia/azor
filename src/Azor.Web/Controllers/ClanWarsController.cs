using Azor.Web.Models.Entities;
using Azor.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Azor.Web.Controllers;

public class ClanWarsController : Controller
{
    private readonly ISeasonStorage _seasonStorage;
    private readonly IBattleReplayStorage _replayStorage;

    public ClanWarsController(ISeasonStorage seasonStorage, IBattleReplayStorage replayStorage)
    {
        _seasonStorage = seasonStorage;
        _replayStorage = replayStorage;
    }
    
    // GET
    public IActionResult Index()
    {
        Season[] seasons = _seasonStorage.GetAll().ToArray();
        return View(seasons);
    }
    
    [HttpGet]
    public IActionResult GetSeason(int id)
    {
        var season = _seasonStorage.GetById(id);
        if (season == null)
            return NotFound();

        return Ok(new
        {
            startDate = season.StartDate,
            endDate = season.EndDate
        });
    }
    
    [HttpGet]
    public IActionResult GetSeasonMatchesInfo(int seasonId)
    {
        var season = _seasonStorage.GetById(seasonId);
        if (season == null) return NotFound();

        var replays = _replayStorage.GetAll()
            .Where(r => r.Metadata.Timestamp >= season.StartDate && r.Metadata.Timestamp <= season.EndDate)
            .Select(r => r.Metadata.Timestamp.Date)
            .Distinct()
            .Select(d => d.ToString("yyyy-MM-dd"))
            .ToList();

        return Ok(new
        {
            startDate = season.StartDate.ToString("yyyy-MM-dd"),
            endDate = season.EndDate.ToString("yyyy-MM-dd"),
            matchDates = replays
        });
    }
    
    [HttpGet]
    public IActionResult GetMatchesByDate(string date)
    {
        var targetDate = DateTime.Parse(date).Date;
        var replays = _replayStorage.GetAll()
            .Where(r => r.Metadata.Timestamp.Date == targetDate)
            .ToList();
        
        var result = replays.Select(r => new {
            map = r.Metadata.Map,
            result = r.Metadata.BattleResult.Type,
            division = r.Division,
            spawn = r.Spawn,
            vehicles = r.Vehicles.Count,
            timestamp = r.Metadata.Timestamp.ToString("HH:mm")
        });

        return Ok(result);
    }
}