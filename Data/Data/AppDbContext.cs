using Data.Entites;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Reflection.Emit;

namespace Data.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUserEntity>(options)
{
    public DbSet<ClientEntity> Clients { get; set; } = null!;

    public DbSet<ProjectEntity> Projects { get; set; } = null!;

    public DbSet<StatusEntity> Statuses { get; set; } = null!;

    public DbSet<ProjectTeamMemberEntity> ProjectTeamMembers { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<ProjectEntity>()
            .HasOne(c => c.Status);




        modelBuilder.Entity<IdentityRole>().HasData(
         new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
         new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" });


        modelBuilder.Entity<StatusEntity>().HasData(
                 new StatusEntity { Id = "1", StatusName = "Started" },
                 new StatusEntity { Id = "2", StatusName = "Completed" });






        modelBuilder.Entity<ProjectTeamMemberEntity>()
            .HasKey(pt => new { pt.ProjectId, pt.AppUserId });

        modelBuilder.Entity<ProjectTeamMemberEntity>()
            .HasOne(pt => pt.Project)
            .WithMany(p => p.ProjectTeamMember)
            .HasForeignKey(pt => pt.ProjectId);

        modelBuilder.Entity<ProjectTeamMemberEntity>()
            .HasOne(pt => pt.AppUser)
            .WithMany(p => p.ProjectTeamMember)
            .HasForeignKey(pt => pt.AppUserId);

    }
}




