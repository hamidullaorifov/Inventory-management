using InventoryApp.Domain.Entities;

namespace InventoryApp.Application.Interfaces.Services;
public interface IAuthService
{
    Task<User> GetAuthenticatedUserAsync();
    Guid GetAuthenticatedUserId();
    string GenerateToken(User user);
}
