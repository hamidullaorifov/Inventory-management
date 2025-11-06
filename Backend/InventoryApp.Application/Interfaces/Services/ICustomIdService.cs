using InventoryApp.Domain.Entities;

namespace InventoryApp.Application.Interfaces.Services;
public interface ICustomIdService
{
    Task<string> GenerateCustomIdAsync(Inventory inventory);
    bool ValidateCustomIdFormat(Inventory inventory, string customId);
}

