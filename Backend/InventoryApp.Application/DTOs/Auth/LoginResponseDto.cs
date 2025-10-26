namespace InventoryApp.Application.DTOs.Auth;
public record LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string Theme { get; set; } = string.Empty;
    public string? ProfilePictureUrl { get; set; }
}
