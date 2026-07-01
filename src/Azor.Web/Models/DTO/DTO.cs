using System.Text.Json.Serialization;

namespace Azor.Web.Models.DTO;

// Ejemplo de DTO para Vehicle (solo las propiedades que nos interesan)
public class VehicleDto
{
    [JsonPropertyName("player")]
    public PlayerDto Player { get; set; }
    
    [JsonPropertyName("index")]
    public string Index { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("nation")]
    public string Nation { get; set; }
    
    [JsonPropertyName("class")]
    public string Class { get; set; }
    
    [JsonPropertyName("tier")]
    public int Tier { get; set; }
    
    [JsonPropertyName("is_test_ship")]
    public bool IsTestShip { get; set; }
    
    [JsonPropertyName("is_enemy")]
    public bool IsEnemy { get; set; }
    
    [JsonPropertyName("captain_id")]
    public string CaptainId { get; set; }
    
    [JsonPropertyName("server_results")]
    public ServerResultsDto ServerResults { get; set; }
    
    [JsonPropertyName("observed_results")]
    public ObservedResultsDto ObservedResults { get; set; }
    
    [JsonPropertyName("time_lived_secs")]
    public double? TimeLivedSecs { get; set; }
    
    [JsonPropertyName("relation")]
    public string Relation { get; set; }
    
    [JsonPropertyName("division_label")]
    public string DivisionLabel { get; set; }
    
    [JsonPropertyName("achievements")]
    public List<string> Achievements { get; set; }
    
    [JsonPropertyName("ribbons")]
    public List<RibbonDto> Ribbons { get; set; }
    
    [JsonPropertyName("personal_rating")]
    public double PersonalRating { get; set; }
    
    [JsonPropertyName("personal_rating_category")]
    public string PersonalRatingCategory { get; set; }
}

public class PlayerDto
{
    [JsonPropertyName("db_id")]
    public long DbId { get; set; }
    
    [JsonPropertyName("realm")]
    public string Realm { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("clan")]
    public string Clan { get; set; }
    
    [JsonPropertyName("division_id")]
    public long DivisionId { get; set; }
    
    [JsonPropertyName("team_id")]
    public int TeamId { get; set; }
    
    [JsonPropertyName("is_replay_perspective")]
    public bool IsReplayPerspective { get; set; }
}

public class ServerResultsDto
{
    [JsonPropertyName("xp")]
    public int Xp { get; set; }
    
    [JsonPropertyName("raw_xp")]
    public int RawXp { get; set; }
    
    [JsonPropertyName("damage")]
    public int Damage { get; set; }
    
    [JsonPropertyName("spotting_damage")]
    public int SpottingDamage { get; set; }
    
    [JsonPropertyName("potential_damage")]
    public int PotentialDamage { get; set; }
    
    [JsonPropertyName("received_damage")]
    public int ReceivedDamage { get; set; }
    
    [JsonPropertyName("fires_dealt")]
    public int FiresDealt { get; set; }
    
    [JsonPropertyName("floods_dealt")]
    public int FloodsDealt { get; set; }
    
    [JsonPropertyName("citadels_dealt")]
    public int CitadelsDealt { get; set; }
    
    [JsonPropertyName("crits_dealt")]
    public int CritsDealt { get; set; }
    
    [JsonPropertyName("distance_traveled")]
    public double DistanceTraveled { get; set; }
    
    [JsonPropertyName("kills")]
    public int Kills { get; set; }
}

public class ObservedResultsDto
{
    [JsonPropertyName("damage")]
    public int Damage { get; set; }
    
    [JsonPropertyName("kills")]
    public int Kills { get; set; }
}

public class RibbonDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; }
    
    [JsonPropertyName("count")]
    public int Count { get; set; }
}

public class MetadataDto
{
    [JsonPropertyName("map")]
    public string Map { get; set; }
    
    [JsonPropertyName("game_mode")]
    public string GameMode { get; set; }
    
    [JsonPropertyName("game_type")]
    public string GameType { get; set; }
    
    [JsonPropertyName("match_group")]
    public string MatchGroup { get; set; }
    
    [JsonPropertyName("version")]
    public VersionDto Version { get; set; }
    
    [JsonPropertyName("played_duration")]
    public double PlayedDuration { get; set; }
    
    [JsonPropertyName("extra_duration")]
    public double ExtraDuration { get; set; }
    
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }
    
    [JsonPropertyName("battle_result")]
    public BattleResultDto BattleResult { get; set; }
}

public class VersionDto
{
    [JsonPropertyName("major")]
    public int Major { get; set; }
    
    [JsonPropertyName("minor")]
    public int Minor { get; set; }
    
    [JsonPropertyName("patch")]
    public int Patch { get; set; }
    
    [JsonPropertyName("build")]
    public int Build { get; set; }
}

public class BattleResultDto
{
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("team_id")]
    public int TeamId { get; set; }
}

public class BattleReplayDto
{
    [JsonPropertyName("vehicles")]
    public List<VehicleDto> Vehicles { get; set; }
    
    [JsonPropertyName("metadata")]
    public MetadataDto Metadata { get; set; }
}