using Data.Data;
using Data.Entites;
using Data.Interfaces;
using Data.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using System.Diagnostics;

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
            Description = entity.Description!,
            EndDate = entity.EndDate,
            StartDate = entity.StartDate,
            Status = new Status
            {
                Id = entity.StatusId,
                StatusName = entity.Status.StatusName
            },
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


    public async Task<RepositoryResult<bool>> UpdateWithAll(ProjectEntity entity)
    {

        try
        {
                var project = await _dbSet
                .Include(e => e.ProjectTeamMember)
                .FirstOrDefaultAsync(e => e.Id == entity.Id);

            if (project is not null)
            {
                _context.Entry(project).CurrentValues.SetValues(entity);
                
                foreach (var member in project.ProjectTeamMember)
                {
                    var updatedMember = entity.ProjectTeamMember.FirstOrDefault(m => m.AppUserId == member.AppUserId);
                    if (updatedMember != null)
                    {
                        _context.Entry(member).CurrentValues.SetValues(updatedMember);
                    }
                    else
                    {
                        _context.Entry(member).State = EntityState.Deleted;
                    }
                }
                foreach (var updatedMember in entity.ProjectTeamMember)
                {
                    var existingMember = project.ProjectTeamMember.FirstOrDefault(m => m.AppUserId == updatedMember.AppUserId);
                    if (existingMember == null)
                    {
                        project.ProjectTeamMember.Add(updatedMember);
                    }
                }
                await _context.SaveChangesAsync();
                return new RepositoryResult<bool> { Success = true, StatusCode = 200 };
            }
            else
            {
                return new RepositoryResult<bool> { Success = false, StatusCode = 404, Error = "Project not found" };
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new RepositoryResult<bool> { Success = false, StatusCode = 500, Error = ex.Message };
        }

    }

}


