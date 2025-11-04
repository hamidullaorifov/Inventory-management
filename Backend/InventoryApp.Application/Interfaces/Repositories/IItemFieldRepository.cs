using InventoryApp.Domain.Entities;

namespace InventoryApp.Application.Interfaces.Repositories;
public interface IItemFieldRepository : IGenericRepository<ItemFieldValue>
{
    Task AddRangeAsync(IEnumerable<ItemFieldValue> fieldValues);
}
