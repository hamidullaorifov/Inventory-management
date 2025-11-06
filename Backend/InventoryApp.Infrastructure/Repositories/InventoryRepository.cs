using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Domain.Entities;
using InventoryApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Infrastructure.Repositories;
public class InventoryRepository(AppDbContext context) : GenericRepository<Inventory>(context), IInventoryRepository
{
    private readonly AppDbContext context = context;
    public new async Task<Inventory> GetByIdAsync(Guid id)
    {
        return await context.Inventories
            .Include(i => i.Tags)
            .FirstOrDefaultAsync(i => i.Id == id);
    }
    public async Task<Inventory> GetDetailsAsync(Guid id)
    {
        return await context.Inventories
            .Include(i => i.Fields)
            .Include(i => i.Category)
            .Include(i => i.Owner)
            .Include(i => i.Tags)
            .Include(i => i.Items)
            .ThenInclude(i => i.FieldValues)
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);
    }
}
