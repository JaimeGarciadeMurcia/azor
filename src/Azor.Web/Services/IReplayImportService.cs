using Azor.Web.Models.Entities;

namespace Azor.Web.Services;

public interface IReplayImportService
{
    Task<BattleReplay> ImportFromStreamAsync(Stream stream);
}