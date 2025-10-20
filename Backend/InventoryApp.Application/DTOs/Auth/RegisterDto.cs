namespace InventoryApp.Application.DTOs.Auth;
public record RegisterDto(string Email, string FullName, string Password, string? ProfilePictureUrl);
