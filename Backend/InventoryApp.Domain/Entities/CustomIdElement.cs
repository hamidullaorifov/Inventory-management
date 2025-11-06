using InventoryApp.Domain.Enums;

namespace InventoryApp.Domain.Entities;
public class CustomIdElement
{
    public Guid Id { get; set; }
    public Guid InventoryId { get; set; }
    public Inventory Inventory { get; set; } = default!;
    public int Order { get; set; }
    public CustomIdElementType Type { get; set; }
    public string? SettingsJson { get; set; }
}

