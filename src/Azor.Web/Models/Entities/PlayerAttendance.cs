namespace Azor.Web.Models.Entities;

public class PlayerAttendance
{
    public long PlayerDbId { get; set; }
    public string PlayerName { get; set; }
    public string Clan { get; set; }
    public int MatchesPlayed { get; set; }
    public int RequiredMatches { get; set; }
    public bool MeetsRequirement { get; set; }
    public double AveragePerDay { get; set; }
}