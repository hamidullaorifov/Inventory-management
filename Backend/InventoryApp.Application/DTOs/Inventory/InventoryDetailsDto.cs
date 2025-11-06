using InventoryApp.Application.DTOs.Auth;
using InventoryApp.Application.DTOs.Item;

namespace InventoryApp.Application.DTOs.Inventory;
public record InventoryDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string Category { get; set; } = default!;
    public string? ImageUrl { get; set; }
    public bool IsPublic { get; set; }
    public Guid OwnerId { get; set; }
    public string OwnerName { get; set; } = default!;
    public DateTime CreatedAt { get; set; }

    public List<string> Tags { get; set; } = [];
    public List<InventoryFieldDto> Fields { get; set; } = [];
    public List<ItemDetailsDto> Items { get; set; } = [];
    public List<UserAccessDto> WriteAccessUsers { get; set; } = [];
}
