using InventoryApp.Domain.Entities;

namespace InventoryApp.Application.Interfaces.Repositories;
public interface IInventoryAccessRepository
{
    Task AddAsync(InventoryAccess inventoryAccess);
    Task DeleteAsync(InventoryAccess inventoryAccess);
    Task<InventoryAccess> GetByUserAndInventoryAsync(Guid UserId, Guid InventoryId);
    Task<List<User>> GetUsersWithAccessToInventoryAsync(Guid InventoryId);
}
