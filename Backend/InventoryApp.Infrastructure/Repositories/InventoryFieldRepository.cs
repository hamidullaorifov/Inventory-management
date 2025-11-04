using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Domain.Entities;
using InventoryApp.Infrastructure.Persistence;

namespace InventoryApp.Infrastructure.Repositories;
public class InventoryFieldRepository(AppDbContext context) : GenericRepository<InventoryFieldDefinition>(context), IInventoryFieldRepository
{
}
