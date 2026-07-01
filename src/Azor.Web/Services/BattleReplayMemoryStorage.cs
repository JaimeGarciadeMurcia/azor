using Azor.Web.Data;
using Azor.Web.Models.Entities;

namespace Azor.Web.Services;

public class BattleReplayMemoryStorage : IBattleReplayStorage
{
    private readonly List<BattleReplay> _replays = new();
    private int _nextReplayId = 1;
    private int _nextVehicleId = 1;
    private int _nextPlayerId = 1;
    private int _nextServerResultsId = 1;
    private int _nextObservedResultsId = 1;
    private int _nextRibbonId = 1;
    private int _nextMetadataId = 1;
    private int _nextVersionId = 1;
    private int _nextBattleResultId = 1;

    public void Add(BattleReplay replay)
    {
        replay.Id = _nextReplayId++;
        
        if (replay.Metadata != null)
        {
            replay.Metadata.Id = _nextMetadataId++;
            if (replay.Metadata.Version != null)
                replay.Metadata.Version.Id = _nextVersionId++;
            if (replay.Metadata.BattleResult != null)
                replay.Metadata.BattleResult.Id = _nextBattleResultId++;
        }
        
        foreach (var vehicle in replay.Vehicles)
        {
            vehicle.Id = _nextVehicleId++;
            vehicle.BattleReplayId = replay.Id;

            if (vehicle.PlayerBattle != null)
            {
                vehicle.PlayerBattle.Id = _nextPlayerId++;
                // Si tuvieras una relación inversa, podrías asignar vehicle.Player.VehicleId = vehicle.Id
            }

            if (vehicle.ServerResults != null)
            {
                vehicle.ServerResults.Id = _nextServerResultsId++;
                // vehicle.ServerResults.VehicleId = vehicle.Id;   // Opcional si existe la relación inversa
            }

            if (vehicle.ObservedResults != null)
            {
                vehicle.ObservedResults.Id = _nextObservedResultsId++;
            }

            foreach (var ribbon in vehicle.Ribbons)
            {
                ribbon.Id = _nextRibbonId++;
                ribbon.VehicleId = vehicle.Id;
            }
        }
        _replays.Add(replay);
    }

    public BattleReplay GetById(int id) => _replays.FirstOrDefault(r => r.Id == id);
    public List<BattleReplay> GetAll() => _replays.ToList();
}