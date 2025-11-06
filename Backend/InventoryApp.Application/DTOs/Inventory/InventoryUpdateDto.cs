namespace InventoryApp.Application.DTOs.Inventory;
public record InventoryUpdateDto
{
    public string Name { get; set; } = default!;
    public string Description { get; set; }
    public Guid CategoryId { get; set; }
    public bool IsPublic { get; set; }
    public string? ImageUrl { get; set; }
    public List<string> Tags { get; set; } = [];
}
