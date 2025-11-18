using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Domain.Entities;
using InventoryApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
// TODO: Learn about ValueTask!
namespace InventoryApp.Infrastructure.Repositories;
public class InventoryAccessRepository(AppDbContext context) : IInventoryAccessRepository
{
    public async Task AddAsync(InventoryAccess inventoryAccess)
    {
        await context.InventoryAccesses.AddAsync(inventoryAccess);
    }

    public Task DeleteAsync(InventoryAccess inventoryAccess)
    {
        context.InventoryAccesses.Remove(inventoryAccess);
        return Task.CompletedTask;
    }

    public async Task<InventoryAccess> GetByUserAndInventoryAsync(Guid UserId, Guid InventoryId)
    {
        return await context.InventoryAccesses
            .FirstOrDefaultAsync(ia => ia.InventoryId == InventoryId && ia.UserId == UserId);
    }

    public async Task<List<User>> GetUsersWithAccessToInventoryAsync(Guid InventoryId)
    {
        return await context.InventoryAccesses
            .Where(ia => ia.InventoryId == InventoryId)
            .Include(ia => ia.User)
            .Select(ia => ia.User)
            .ToListAsync();
    }
}
