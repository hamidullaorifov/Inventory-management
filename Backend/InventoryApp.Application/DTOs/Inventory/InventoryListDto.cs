namespace InventoryApp.Application.DTOs.Inventory;
public class InventoryListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid OwnerId { get; set; } = default!;
    public string OwnerName { get; set; }
    public List<string> Tags { get; set; }
}
