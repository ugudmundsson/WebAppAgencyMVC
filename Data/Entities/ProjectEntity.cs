using Data.Entities;
using Data.Migrations;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entites;

public class ProjectEntity

{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [DataType(DataType.Upload)]
    public string? Image { get; set; }
    public string ProjectName { get; set; } = null!;

    public string ClientName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [ForeignKey(nameof(Status))]
    public string StatusId { get; set; } = null!;
    public virtual StatusEntity Status { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Budget { get; set; }

    public virtual ICollection<ProjectTeamMemberEntity> ProjectTeamMember { get; set; } = [];

}
