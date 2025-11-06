using InventoryApp.Domain.Entities;

namespace InventoryApp.Application.Interfaces.Repositories;
public interface IItemLikeRepository
{
    Task AddAsync(ItemLike itemLike);
    Task DeleteAsync(ItemLike itemLike);
    Task<ItemLike> GetByItemAndUserAsync(Guid itemId, Guid userId);
}
