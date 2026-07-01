using System.ComponentModel.DataAnnotations;

namespace Azor.Web.Models.Entities;

/// <summary>
/// Resultados observados (pueden diferir ligeramente de ServerResults)
/// </summary>
public class ObservedResults
{
    [Key]
    public int Id { get; set; }
    
    public int Damage { get; set; }
    public int Kills { get; set; }
}