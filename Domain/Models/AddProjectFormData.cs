namespace Domain.Models;

public class AddProjectFormData
{
    public string? Image { get; set; }
    public string ProjectName { get; set; } = null!;
    public string ClientName { get; set; } = null!;
    public string? Description { get; set; }
    public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public string? ClientId { get; set; }
    public List<string> AppUserId { get; set; } = [];
    public int? StatusId { get; set; }
    public decimal? Budget { get; set; }
}
