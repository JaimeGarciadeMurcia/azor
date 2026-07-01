using System.ComponentModel.DataAnnotations;

namespace Azor.Web.Models.Entities;

/// <summary>
/// Metadatos de la batalla (mapa, duración, resultado, etc.)
/// </summary>
public class Metadata
{
    [Key]
    public int Id { get; set; }
    
    public string Map { get; set; }            // "Trident"
    public string GameMode { get; set; }       // "Clans. Domination"
    public string GameType { get; set; }       // "ClanBattle"
    public string MatchGroup { get; set; }     // "clan"
    public double PlayedDuration { get; set; } // segundos jugados
    public double ExtraDuration { get; set; }  // tiempo extra
    public DateTime Timestamp { get; set; }    // UTC
    
    public virtual VersionInfo Version { get; set; }
    public virtual BattleResult BattleResult { get; set; }
    
    // Claves foráneas
    public int VersionId { get; set; }
    public int BattleResultId { get; set; }
}