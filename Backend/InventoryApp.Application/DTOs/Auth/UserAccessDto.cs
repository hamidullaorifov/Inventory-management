namespace InventoryApp.Application.DTOs.Auth;
public record UserAccessDto
{
    public Guid UserId { get; set; }
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
}
