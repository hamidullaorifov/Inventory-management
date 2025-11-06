namespace InventoryApp.Application.Interfaces.Repositories;
public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IInventoryRepository InventoryRepository { get; }
    IItemRepository ItemRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    ITagRepository TagRepository { get; }
    IInventoryFieldRepository InventoryFieldRepository { get; }
    IItemFieldRepository ItemFieldRepository { get; }
    IItemLikeRepository ItemLikeRepository { get; }
    IInventoryAccessRepository InventoryAccessRepository { get; }
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
