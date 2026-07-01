using System.ComponentModel.DataAnnotations;

namespace Azor.Web.Models.Entities;

/// <summary>
/// Resultado de la batalla (victoria/derrota y equipo ganador)
/// </summary>
public class BattleResult
{
    [Key]
    public int Id { get; set; }
    
    public string Type { get; set; }  // "Win", "Loss", etc.
    public int TeamId { get; set; }   // Número de equipo que ganó (1 o 0)
}