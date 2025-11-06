namespace InventoryApp.Application.DTOs.Inventory;
public record AddInventoryAccessDto
{
    public Guid UserId { get; set; }
    public bool CanWrite { get; set; }
}
