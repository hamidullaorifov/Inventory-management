using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Domain.Entities;
using InventoryApp.Infrastructure.Persistence;

namespace InventoryApp.Infrastructure.Repositories;
public class UserRepository(AppDbContext context) : GenericRepository<User>(context), IUserRepository
{
}
