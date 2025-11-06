using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Domain.Entities;
using InventoryApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Infrastructure.Repositories;
public class ItemLikeRepository(AppDbContext context) : IItemLikeRepository
{
    public async Task AddAsync(ItemLike itemLike)
    {
        await context.ItemLikes.AddAsync(itemLike);
    }

    public Task DeleteAsync(ItemLike itemLike)
    {
        context.ItemLikes.Remove(itemLike);
        return Task.CompletedTask;
    }

    public async Task<ItemLike> GetByItemAndUserAsync(Guid itemId, Guid userId)
    {
        return await context.ItemLikes
            .FirstOrDefaultAsync(il => il.ItemId == itemId && il.UserId == userId);
    }
}
