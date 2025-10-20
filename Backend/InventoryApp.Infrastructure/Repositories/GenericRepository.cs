using System.Linq.Expressions;
using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Domain.Common;
using InventoryApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Infrastructure.Repositories;
public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : BaseEntity
{
    public async Task AddAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
    }

    public Task DeleteAsync(T entity)
    {
        context.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return await context.Set<T>().AnyAsync(predicate);
    }

    public IQueryable<T> Query(Expression<Func<T, bool>>? predicate = null)
    {
        var query = context.Set<T>().AsNoTracking();
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        return query;
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public Task UpdateAsync(T entity)
    {
        context.Set<T>().Update(entity);
        return Task.CompletedTask;
    }
}
