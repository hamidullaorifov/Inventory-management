using InventoryApp.Domain.Common;

namespace InventoryApp.Domain.Entities;
public class User : AuditableEntity
{
    public string Email { get; set; }
    public string FullName { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public bool IsBlocked { get; set; }
    public bool IsAdmin { get; set; }
    public string PasswordHash { get; set; }
    public string Language { get; set; } = "en";
    public string Theme { get; set; } = "light";

    // Navigation property for related items
    public ICollection<Inventory> Inventories { get; set; } = [];
}
