using System.ComponentModel.DataAnnotations;

namespace Azor.Web.Models.Entities;

/// <summary>
/// Versión del juego
/// </summary>
public class VersionInfo
{
    [Key]
    public int Id { get; set; }
    
    public int Major { get; set; }  // 15
    public int Minor { get; set; }  // 3
    public int Patch { get; set; }  // 0
    public int Build { get; set; }  // 12267945
}