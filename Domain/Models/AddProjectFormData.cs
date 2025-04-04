namespace Domain.Models;

public class AddProjectFormData
{
    public string? Image { get; set; } = null!;
    public string ProjectName { get; set; } = null!;

    public string ClientName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
   
    public decimal Budget { get; set; }

    public string ClientId { get; set; } = null!;

    public string AppUserId { get; set; } = null!;

    public int StatusId { get; set; }
}
