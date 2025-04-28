using Domain.Models;
namespace WebAppAssignment.Models;

public class ProjectViewModel
{
    public AddProjectModel Form { get; set; } = new AddProjectModel();

    public IEnumerable<Status> Statuses { get; set; } = [];

    public IEnumerable<AppUser> TeamMembers { get; set; } = [];

    public IEnumerable<Project> Projects { get; set; } = [];
    public Dictionary<string, int> StatusCounts { get; set; } = new Dictionary<string, int>();

}
