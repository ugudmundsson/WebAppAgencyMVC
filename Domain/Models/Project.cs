namespace Domain.Models;

public class Project
{
    public string Id { get; set; } = null!;
    public string? Image { get; set; }
    public string ProjectName { get; set; } = null!;
    public string ClientName { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public AppUser User { get; set; } = null!;
    public Status Status { get; set; } = null!;
    public decimal? Budget { get; set; }
    public List<string> TeamMembers { get; set; } = new List<string>();
    public int DaysLeft => (EndDate - DateTime.Now).Days;

}
