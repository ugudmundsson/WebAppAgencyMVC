using Data.Data;
using Data.Entites;
using Data.Interfaces;
using Data.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace Data.Repositories;

public class ProjectRepository(AppDbContext context) : BaseRepository<ProjectEntity, Project>(context), IProjectRepository
{

    public override async Task<RepositoryResult<IEnumerable<Project>>> GetAllAsync(
    bool orderByDescending = false,
    Expression<Func<ProjectEntity, object>>? sortBy = null,
    Expression<Func<ProjectEntity, bool>>? where = null,
    params Expression<Func<ProjectEntity, object>>[] includes)
    {
        IQueryable<ProjectEntity> query = _dbSet;

        if (where != null)
            query = query.Where(where);

        if (includes != null && includes.Length > 0)
            foreach (var include in includes)
                query = query.Include(include);

        query = query.Include(e => e.ProjectTeamMember).ThenInclude(pt => pt.AppUser);
        

        if (sortBy != null)
            query = orderByDescending ? query.OrderByDescending(sortBy) : query.OrderBy(sortBy);

        var entities = await query.ToListAsync();

        var result = entities.Select(entity => new Project
        {
            Id = entity.Id,
            ProjectName = entity.ProjectName,
            ClientName = entity.ClientName,
            Description = entity.Description,
            EndDate = entity.EndDate,
            StartDate = entity.StartDate,
            TeamMembers = entity.ProjectTeamMember
                .Select(pt => pt.AppUser.FirstName).ToList()
        }).ToList();
        return new RepositoryResult<IEnumerable<Project>>
        {
            Success = true,
            StatusCode = 200,
            Result = result
        };
    }

}


