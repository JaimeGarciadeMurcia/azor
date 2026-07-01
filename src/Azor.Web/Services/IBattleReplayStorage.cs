using Azor.Web.Models.Entities;

namespace Azor.Web.Services;

public interface IBattleReplayStorage
{
    void Add(BattleReplay replay);
    BattleReplay? GetById(int id);
    List<BattleReplay> GetAll();
}