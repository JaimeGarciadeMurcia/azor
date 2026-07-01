using System.ComponentModel.DataAnnotations;

namespace Azor.Web.Models.Entities;

/// <summary>
/// Representa un vehículo (barco) en la batalla
/// </summary>
public class Vehicle
{
    [Key]
    public int Id { get; set; }  // Clave primaria artificial
    
    [Required]
    public string Index { get; set; }          // ej: "PRSC208"
    public string Name { get; set; }           // ej: "Tallinn"
    public string Nation { get; set; }         // "Russia", "USA", etc.
    public string Class { get; set; }          // "Cruiser", "Battleship", "Destroyer"
    public int Tier { get; set; }              // 8, etc.
    public bool IsTestShip { get; set; }
    public bool IsEnemy { get; set; }          // true = enemigo, false = aliado
    public string CaptainId { get; set; }      // ej: "PRW407"
    public double? TimeLivedSecs { get; set; } // segundos vividos (si es null, el vehículo estuvo vivo toda la partida)
    public string Relation { get; set; }       // "ally", "self", "enemy"
    public string DivisionLabel { get; set; }  // "(A)", "(B)", etc.
    public double PersonalRating { get; set; }
    public string PersonalRatingCategory { get; set; } // "Super Unicum", "Great", etc.
    
    // Colecciones simples (lista de strings para achievements)
    public ICollection<string> Achievements { get; set; } = new List<string>();
    
    // Relaciones con otras entidades
    public virtual PlayerBattle PlayerBattle { get; set; }
    public virtual ServerResults ServerResults { get; set; }
    public virtual ObservedResults? ObservedResults { get; set; }
    public virtual ICollection<Ribbon> Ribbons { get; set; } = new List<Ribbon>();
    
    // Claves foráneas explícitas (opcional pero útil para EF)
    public int PlayerId { get; set; }
    public int ServerResultsId { get; set; }
    public int? ObservedResultsId { get; set; }
    
    // Clave foránea a BattleReplay (relación inversa)
    public int BattleReplayId { get; set; }
    public virtual BattleReplay BattleReplay { get; set; }
}