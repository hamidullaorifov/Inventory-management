namespace InventoryApp.Domain.Entities;
public class InventoryAccess
{
    public Guid InventoryId { get; set; }
    public Inventory Inventory { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public bool CanWrite { get; set; } = true;
}
