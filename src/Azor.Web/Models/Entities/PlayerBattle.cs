using System.ComponentModel.DataAnnotations;

namespace Azor.Web.Models.Entities;

/// <summary>
/// Datos del jugador (perfil)
/// </summary>
public class PlayerBattle
{
    [Key]
    public int Id { get; set; }
    public long DbId { get; set; }             // ID único del jugador en el juego
    public string Realm { get; set; }          // "EU", "NA", etc.
    public string Name { get; set; }
    public string Clan { get; set; }
    public long DivisionId { get; set; }       // ID de la división en el clan
    public int TeamId { get; set; }            // 0 = enemigo, 1 = aliado
    public bool IsReplayPerspective { get; set; } // true si es el punto de vista de la repetición
}