using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Infrastructure.Persistence;

namespace InventoryApp.Infrastructure.Repositories;
public class UnitOfWork(
    IUserRepository userRepository,
    IInventoryRepository inventoryRepository,
    ICategoryRepository categoryRepository,
    ITagRepository tagRepository,
    IInventoryFieldRepository inventoryFieldRepository,
    IItemFieldRepository itemFieldRepository,
    IItemRepository itemRepository,
    AppDbContext context) : IUnitOfWork
{
    public AppDbContext Context => context;
    public IUserRepository UserRepository => userRepository;

    public IInventoryRepository InventoryRepository => inventoryRepository;

    public ICategoryRepository CategoryRepository => categoryRepository;

    public IInventoryFieldRepository InventoryFieldRepository => inventoryFieldRepository;
    public IItemFieldRepository ItemFieldRepository => itemFieldRepository;
    public IItemRepository ItemRepository => itemRepository;

    public ITagRepository TagRepository => tagRepository;

    public IInventoryAccessRepository InventoryAccessRepository => throw new NotImplementedException();

    public Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}
