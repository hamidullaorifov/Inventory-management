using System.Linq.Expressions;
using InventoryApp.Domain.Common;

namespace InventoryApp.Application.Interfaces.Repositories;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(Guid id);
    IQueryable<T> Query(Expression<Func<T, bool>>? predicate = null);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
}
