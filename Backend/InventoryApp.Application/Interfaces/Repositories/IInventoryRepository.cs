using InventoryApp.Domain.Entities;

namespace InventoryApp.Application.Interfaces.Repositories;
public interface IInventoryRepository : IGenericRepository<Inventory>
{
    Task<Inventory> GetDetailsAsync(Guid id);
}
