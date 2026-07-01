using System.ComponentModel.DataAnnotations;

namespace Azor.Web.Models.Entities;

/// <summary>
/// Cinta / logro (ejemplo: "Penetration", "Citadel hits")
/// </summary>
public class Ribbon
{
    [Key]
    public int Id { get; set; }
    
    public string Name { get; set; }         // "RIBBON_MAIN_CALIBER_PENETRATION"
    public string DisplayName { get; set; }  // "Penetration"
    public int Count { get; set; }
    
    // Relación con Vehicle
    public int VehicleId { get; set; }
    public virtual Vehicle Vehicle { get; set; }
}