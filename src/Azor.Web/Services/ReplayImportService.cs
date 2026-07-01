using System.Text.Json;
using Azor.Web.Models.DTO;
using Azor.Web.Models.Entities;

namespace Azor.Web.Services;

public class ReplayImportService : IReplayImportService
{
    public async Task<BattleReplay> ImportFromStreamAsync(Stream stream)
    {
        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();
        var battleReplayDto = JsonSerializer.Deserialize<BattleReplayDto>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (battleReplayDto == null)
            throw new InvalidOperationException("Failed to deserialize JSON");

        var battleReplay = new BattleReplay
        {
            Metadata = MapMetadata(battleReplayDto.Metadata),
            Vehicles = new List<Vehicle>()
        };

        foreach (var vehicleDto in battleReplayDto.Vehicles)
        {
            var vehicle = MapVehicle(vehicleDto);
            vehicle.BattleReplay = battleReplay;
            battleReplay.Vehicles.Add(vehicle);
        }

        return battleReplay;
    }

    private Metadata MapMetadata(MetadataDto dto)
    {
        if (dto == null) return null;
        return new Metadata
        {
            Map = dto.Map,
            GameMode = dto.GameMode,
            GameType = dto.GameType,
            MatchGroup = dto.MatchGroup,
            PlayedDuration = dto.PlayedDuration,
            ExtraDuration = dto.ExtraDuration,
            Timestamp = dto.Timestamp,
            Version = new VersionInfo
            {
                Major = dto.Version.Major,
                Minor = dto.Version.Minor,
                Patch = dto.Version.Patch,
                Build = dto.Version.Build
            },
            BattleResult = new BattleResult
            {
                Type = dto.BattleResult.Type,
                TeamId = dto.BattleResult.TeamId
            }
        };
    }

    private Vehicle MapVehicle(VehicleDto dto)
    {
        var vehicle = new Vehicle
        {
            Index = dto.Index,
            Name = dto.Name,
            Nation = dto.Nation,
            Class = dto.Class,
            Tier = dto.Tier,
            IsTestShip = dto.IsTestShip,
            IsEnemy = dto.IsEnemy,
            CaptainId = dto.CaptainId,
            TimeLivedSecs = dto.TimeLivedSecs,
            Relation = dto.Relation,
            DivisionLabel = dto.DivisionLabel,
            PersonalRating = dto.PersonalRating,
            PersonalRatingCategory = dto.PersonalRatingCategory,
            Achievements = dto.Achievements ?? new List<string>(),
            PlayerBattle = MapPlayer(dto.Player),
            ServerResults = MapServerResults(dto.ServerResults),
            ObservedResults = dto.ObservedResults != null ? MapObservedResults(dto.ObservedResults) : null,
            Ribbons = dto.Ribbons?.Select(r => new Ribbon
            {
                Name = r.Name,
                DisplayName = r.DisplayName,
                Count = r.Count
            }).ToList() ?? new List<Ribbon>()
        };
        // Relaciones inversas (se asignan después para evitar ciclos)
        if (vehicle.PlayerBattle != null) vehicle.PlayerId = vehicle.PlayerBattle.Id;
        if (vehicle.ServerResults != null) vehicle.ServerResultsId = vehicle.ServerResults.Id;
        if (vehicle.ObservedResults != null) vehicle.ObservedResultsId = vehicle.ObservedResults.Id;
        return vehicle;
    }

    private PlayerBattle MapPlayer(PlayerDto dto)
    {
        if (dto == null) return null;
        return new PlayerBattle
        {
            DbId = dto.DbId,
            Realm = dto.Realm,
            Name = dto.Name,
            Clan = dto.Clan,
            DivisionId = dto.DivisionId,
            TeamId = dto.TeamId,
            IsReplayPerspective = dto.IsReplayPerspective
        };
    }

    private ServerResults MapServerResults(ServerResultsDto dto)
    {
        if (dto == null) return null;
        return new ServerResults
        {
            Xp = dto.Xp,
            RawXp = dto.RawXp,
            Damage = dto.Damage,
            SpottingDamage = dto.SpottingDamage,
            PotentialDamage = dto.PotentialDamage,
            ReceivedDamage = dto.ReceivedDamage,
            FiresDealt = dto.FiresDealt,
            FloodsDealt = dto.FloodsDealt,
            CitadelsDealt = dto.CitadelsDealt,
            CritsDealt = dto.CritsDealt,
            DistanceTraveled = dto.DistanceTraveled,
            Kills = dto.Kills
        };
    }

    private ObservedResults MapObservedResults(ObservedResultsDto dto)
    {
        return new ObservedResults
        {
            Damage = dto.Damage,
            Kills = dto.Kills
        };
    }
}