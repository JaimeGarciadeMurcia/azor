using System.ComponentModel.DataAnnotations;

namespace Azor.Web.Models.Entities;

/// <summary>
/// Contenedor raíz de la batalla (equivalente al objeto JSON raíz)
/// </summary>
public class BattleReplay
{
    [Key]
    public int Id { get; set; } 
    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    public virtual Metadata Metadata { get; set; }
    public int? SeasonId { get; set; }
    public virtual Season? Season { get; set; }
    public string Division { get; set; }  // "Alpha", "Bravo", etc.
    public string Spawn { get; set; }     // "Norte", "Sur".
}