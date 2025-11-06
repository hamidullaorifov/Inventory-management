using InventoryApp.Domain.Common;

namespace InventoryApp.Domain.Entities;

public class Item : AuditableEntity
{
    public Guid InventoryId { get; set; }
    public Inventory Inventory { get; set; }
    public string CustomId { get; set; }
    public int? SequenceNumber { get; set; } // for sequence element tracking
    public Guid CreatedById { get; set; }
    public User CreatedBy { get; set; }
    public Guid UpdatedById { get; set; }
    public User UpdatedBy { get; set; }
    public ICollection<ItemFieldValue> FieldValues { get; set; } = [];
}