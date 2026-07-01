using Azor.Web.Data;
using Azor.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Azor.Web.Services;

public class BattleReplayDbStorage : IBattleReplayStorage
{
    private readonly ApplicationDbContext _context;

    public BattleReplayDbStorage(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(BattleReplay replay)
    {
        await _context.BattleReplays.AddAsync(replay);
        await _context.SaveChangesAsync();
    }

    public void Add(BattleReplay replay)
    {
        if (Exists(replay).Result)
        {
            _context.BattleReplays.Add(replay);
            _context.SaveChangesAsync();
        }
    }

    public async Task<bool> Exists(BattleReplay replay)
    {
        return await _context.BattleReplays.FindAsync(replay) is not null;
    }

    public BattleReplay? GetById(int id)
    {
        return _context.BattleReplays
            .Include(r => r.Metadata)
            .ThenInclude(m => m.Version)
            .Include(r => r.Metadata)
            .ThenInclude(m => m.BattleResult)
            .Include(r => r.Vehicles)
            .ThenInclude(v => v.PlayerBattle)
            .Include(r => r.Vehicles)
            .ThenInclude(v => v.ServerResults)
            .Include(r => r.Vehicles)
            .ThenInclude(v => v.ObservedResults)
            .Include(r => r.Vehicles)
            .ThenInclude(v => v.Ribbons)
            .FirstOrDefault(r => r.Id == id);
    }

    public List<BattleReplay> GetAll()
    {
        return _context.BattleReplays
            .Include(r => r.Metadata)
            .Include(r => r.Vehicles)
            .ThenInclude(v => v.PlayerBattle)
            .ToList();
    }
}