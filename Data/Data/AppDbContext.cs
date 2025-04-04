using Data.Entites;
using Data.Entities;
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


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StatusEntity>().HasData(
                 new StatusEntity { Id = 1, StatusName = "Started" },
                 new StatusEntity { Id = 2, StatusName = "Completed" });
    }
}




