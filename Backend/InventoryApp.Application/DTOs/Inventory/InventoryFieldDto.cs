namespace InventoryApp.Application.DTOs.Inventory;
public record InventoryFieldDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public string Type { get; set; } = default!;
    public bool ShowInTable { get; set; }
}
