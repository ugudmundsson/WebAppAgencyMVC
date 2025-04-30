using Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppAssignment.Models;

public class AddProjectModel
{
    public string Id { get; set; } = null!;

    [DataType(DataType.Upload)]
    public IFormFile? Image { get; set; }

    [Required(ErrorMessage = "Project Name is required")]
    [Display(Name = "Project Name", Prompt = "Project Name")]
    public string ProjectName { get; set; } = null!;

    [Required(ErrorMessage = "Client Name is required")]
    [Display(Name = "Client Name", Prompt = "Client Name")]
    public string ClientName { get; set; } = null!;

    
    [Display(Name = "Description", Prompt = "Type something")]
    public string? Description { get; set; }


    [Display(Name = "Start Date", Prompt = "Start Date")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; } = DateTime.Now;


    [Display(Name = "End Date", Prompt = "End Date")]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Team Members is required")]
    [Display(Name = "Members", Prompt = "Members")]
    public List<string> Members { get; set; } = [];

    [Display(Name = "Status", Prompt = "Status")]
    public string StatusId { get; set; } = null!;

    [Display(Name = "Budget", Prompt = "$ 0")]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Budget { get; set; }

}
