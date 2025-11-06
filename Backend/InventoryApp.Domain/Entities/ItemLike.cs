namespace InventoryApp.Domain.Entities;
public class ItemLike
{
    public Guid ItemId { get; set; }
    public Item Item { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public DateTime LikedAt { get; set; } = DateTime.UtcNow;
}
