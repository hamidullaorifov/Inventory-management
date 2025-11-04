using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Domain.Entities;
using InventoryApp.Infrastructure.Persistence;

namespace InventoryApp.Infrastructure.Repositories;
public class ItemFieldRepository(AppDbContext context) : GenericRepository<ItemFieldValue>(context), IItemFieldRepository
{
    private readonly AppDbContext context = context;
    public async Task AddRangeAsync(IEnumerable<ItemFieldValue> fieldValues)
    {
        await context.ItemFieldValues.AddRangeAsync(fieldValues);
    }
}
