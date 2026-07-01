namespace Azor.Web.Models.Entities;

public class Season
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int TotalDays => (EndDate - StartDate).Days + 1; // días inclusive
    
    //Las partidas de temporada de clanes son miércoles, jueves, sábado y domingo.
}