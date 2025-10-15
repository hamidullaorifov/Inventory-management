using InventoryApp.Domain.Common;
using InventoryApp.Domain.Enums;

namespace InventoryApp.Domain.Entities;
public class InventoryFieldDefinition : BaseEntity
{
    public Guid InventoryId { get; set; }
    public Inventory Inventory { get; set; }
    public FieldType Type { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool ShowInTable { get; set; }
}
