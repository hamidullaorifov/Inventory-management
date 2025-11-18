using System.Security.Claims;
using InventoryApp.Domain.Entities;

namespace InventoryApp.Application.Interfaces.Services;
public interface IAuthService
{
    Guid GetAuthenticatedUserId();
    string GenerateToken(User user);
    Task<bool> CanEditInventory(Inventory inventory);
    Task<bool> CanManageAccess(Inventory inventory);
    Task<bool> CanAddOrEditItem(Inventory inventory);
    bool IsAdmin(ClaimsPrincipal user);
}
