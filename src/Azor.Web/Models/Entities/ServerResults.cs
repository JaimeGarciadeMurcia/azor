using System.ComponentModel.DataAnnotations;

namespace Azor.Web.Models.Entities;

/// <summary>
/// Resultados medidos por el servidor
/// </summary>
public class ServerResults
{
    [Key]
    public int Id { get; set; }
    
    public int Xp { get; set; }
    public int RawXp { get; set; }
    public int Damage { get; set; }
    public int SpottingDamage { get; set; }
    public int PotentialDamage { get; set; }
    public int ReceivedDamage { get; set; }
    public int FiresDealt { get; set; }
    public int FloodsDealt { get; set; }
    public int CitadelsDealt { get; set; }
    public int CritsDealt { get; set; }
    public double DistanceTraveled { get; set; } // kilómetros o metros según datos
    public int Kills { get; set; }
}