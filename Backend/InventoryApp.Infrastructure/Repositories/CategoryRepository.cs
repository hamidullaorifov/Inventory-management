using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Domain.Entities;
using InventoryApp.Infrastructure.Persistence;

namespace InventoryApp.Infrastructure.Repositories;
public class CategoryRepository(AppDbContext context) : GenericRepository<InventoryCategory>(context), ICategoryRepository
{
}
