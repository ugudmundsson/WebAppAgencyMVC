using Data.Data;
using Data.Interfaces;
using Data.Migrations;
using Data.Models;
using Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity, T> : IBaseRepository<TEntity, T> where TEntity : class
{

    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }





    //CREATE-------------------------------------------------
    public virtual async Task<RepositoryResult<bool>> AddAsync(TEntity entity)
    {
        if (entity == null)
            return new RepositoryResult<bool> { Success = false, StatusCode = 400, Error = "Entity is null" };
        try
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { Success = true, StatusCode = 201 };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new RepositoryResult<bool> { Success = false, StatusCode = 500, Error = ex.Message };
        }
    }





    //READ-------------------------------------------------
    public virtual async Task<RepositoryResult<IEnumerable<T>>> GetAllAsync
        (bool orderByDescending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? where = null, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;

        if (where != null)
            query = query.Where(where);

        if (includes != null && includes.Length != 0)
            foreach (var include in includes)
                query = query.Include(include);
        if (sortBy != null)
            query = orderByDescending
                ? query.OrderByDescending(sortBy)
                : query.OrderBy(sortBy);

        var entities = await query.ToListAsync();
        var resault = entities.Select(entity => entity.MapTo<T>());
        return new RepositoryResult<IEnumerable<T>> { Success = true, StatusCode = 200, Result = resault };
    }





    public virtual async Task<RepositoryResult<IEnumerable<TSelect>>> GetAllAsync<TSelect>
    (Expression<Func<TEntity, TSelect>> selector, bool orderByDescending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? where = null, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;

        if (where != null)
            query = query.Where(where);

        if (includes != null && includes.Length != 0)
            foreach (var include in includes)
                query = query.Include(include);

        if (sortBy != null)
            query = orderByDescending
                ? query.OrderByDescending(sortBy)
                : query.OrderBy(sortBy);

        var entities = await query.Select(selector).ToListAsync();
        var result = entities.Select(entity => entity!.MapTo<TSelect>());
        return new RepositoryResult<IEnumerable<TSelect>> { Success = true, StatusCode = 200, Result = result };
    }









    public virtual async Task<RepositoryResult<T>> GetAsync(Expression<Func<TEntity, bool>> where = null!, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;

        if (includes != null && includes.Length != 0)
            foreach (var include in includes)
                query = query.Include(include);

        var entity = await query.FirstOrDefaultAsync(where);


        if (entity == null)
            return new RepositoryResult<T> { Success = false, StatusCode = 404, Error = "Entity not found" };

        var result = entity.MapTo<T>();
        return new RepositoryResult<T> { Success = true, StatusCode = 200, Result = result };
    }










    public virtual async Task<RepositoryResult<bool>> ExistsAsync(Expression<Func<TEntity, bool>> findBy)
    {
        var exists = await _dbSet.AnyAsync(findBy);
        return !exists
            ? new RepositoryResult<bool> { Success = false, StatusCode = 404, Error = "Entity not found" }
            : new RepositoryResult<bool> { Success = true, StatusCode = 200 };

    }





    //UPDATE-------------------------------------------------
    public virtual async Task<RepositoryResult<bool>> UpdateAsync(TEntity entity)
    {
        if (entity == null)
            return new RepositoryResult<bool> { Success = false, StatusCode = 400, Error = "Entity is null" };
        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { Success = true, StatusCode = 200 };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new RepositoryResult<bool> { Success = false, StatusCode = 500, Error = ex.Message };
        }
    }






    //DELETE-------------------------------------------------
    public virtual async Task<RepositoryResult<bool>> RemoveAsync(TEntity entity)
    {
        if (entity == null)
            return new RepositoryResult<bool> { Success = false, StatusCode = 400, Error = "Entity is null" };
        try
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { Success = true, StatusCode = 200 };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new RepositoryResult<bool> { Success = false, StatusCode = 500, Error = ex.Message };
        }
    }



    // SAVE CHANGES-------------------------------------------------


}
