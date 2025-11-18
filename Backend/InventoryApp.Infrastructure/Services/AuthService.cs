using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Application.Interfaces.Services;
using InventoryApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
#pragma warning disable IDE0011
namespace InventoryApp.Infrastructure.Services;
public class AuthService(
    IConfiguration configuration,
    IHttpContextAccessor httpContextAccessor,
    IUnitOfWork unitOfWork) : IAuthService
{
    public async Task<bool> CanAddOrEditItem(Inventory inventory)
    {
        return await CanEditInventory(inventory);
    }

    public async Task<bool> CanEditInventory(Inventory inventory)
    {
        var user = GetCurrentUser();
        if (IsAdmin(user))
        {
            return true;
        }

        var userId = GetAuthenticatedUserId();
        if (inventory.OwnerId == userId) return true;
        if (inventory.IsPublic && user.Identity!.IsAuthenticated) return true;
        var inventoryAccess = await unitOfWork.InventoryAccessRepository.GetByUserAndInventoryAsync(userId, inventory.Id);
        return inventoryAccess != null && inventoryAccess.CanWrite;
    }

    public Task<bool> CanManageAccess(Inventory inventory)
    {
        var user = GetCurrentUser();

        if (IsAdmin(user)) return Task.FromResult(true);

        var userId = GetAuthenticatedUserId();
        return Task.FromResult(inventory.OwnerId == userId);
    }

    public string GenerateToken(User user)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("FullName", user.FullName),
                new Claim("IsAdmin", user.IsAdmin.ToString())
            };

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiresInMinutes"]!)),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public Guid GetAuthenticatedUserId()
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(userId, out var id) ? id : Guid.Empty;
    }

    public bool IsAdmin(ClaimsPrincipal user)
    {
        return user.IsInRole("Admin");
    }
    private ClaimsPrincipal GetCurrentUser()
    {
        return httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
    }
}
