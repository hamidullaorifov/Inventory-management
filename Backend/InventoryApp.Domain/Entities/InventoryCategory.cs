using InventoryApp.Domain.Common;

namespace InventoryApp.Domain.Entities;
public class InventoryCategory : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Inventory> Inventories { get; set; } = [];
}
