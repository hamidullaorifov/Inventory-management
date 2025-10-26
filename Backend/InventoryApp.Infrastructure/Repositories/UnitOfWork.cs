using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Infrastructure.Persistence;

namespace InventoryApp.Infrastructure.Repositories;
public class UnitOfWork(
    IUserRepository userRepository,
    IInventoryRepository inventoryRepository,
    AppDbContext context) : IUnitOfWork
{
    public IUserRepository UserRepository => userRepository;

    public IInventoryRepository InventoryRepository => inventoryRepository;

    public IItemRepository ItemRepository => throw new NotImplementedException();

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
