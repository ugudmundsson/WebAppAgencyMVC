namespace Domain.Models;

public class AddProjectFormData
{
    public string? Image { get; set; }
    public string ProjectName { get; set; } = null!;
    public string ClientName { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<string> Members { get; set; } = [];
    public int StatusId { get; set; }
    public decimal? Budget { get; set; }
}
