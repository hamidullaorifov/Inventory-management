namespace InventoryApp.Application.DTOs.Inventory;
public record InventoryCreateDto
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public string CustomIdFormatJson { get; set; }
    public List<string> Tags { get; set; } = [];
}
