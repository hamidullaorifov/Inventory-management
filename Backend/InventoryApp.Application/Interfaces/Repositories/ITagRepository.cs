using System.Linq.Expressions;
using InventoryApp.Domain.Entities;

namespace InventoryApp.Application.Interfaces.Repositories;
public interface ITagRepository
{
    Task<List<Tag>> GetAllAsync(Expression<Func<Tag, bool>> predicate);
    Task AddRangeAsync(IEnumerable<Tag> tags);
}
