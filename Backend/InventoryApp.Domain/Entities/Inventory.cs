using InventoryApp.Domain.Common;

namespace InventoryApp.Domain.Entities;
public class Inventory : AuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsPublic { get; set; }
    public Guid OwnerId { get; set; }
    public User Owner { get; set; }
    public Guid CategoryId { get; set; }
    public InventoryCategory Category { get; set; }
    public ICollection<Item> Items { get; set; } = [];

}
