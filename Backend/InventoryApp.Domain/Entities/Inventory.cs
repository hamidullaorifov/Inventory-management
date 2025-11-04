using System.ComponentModel.DataAnnotations;
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
    public string CustomIdFormatJson { get; set; }
    public Guid CategoryId { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }
    public InventoryCategory Category { get; set; }
    public ICollection<Item> Items { get; set; } = [];
    public ICollection<InventoryFieldDefinition> Fields { get; set; } = [];
    public ICollection<Tag> Tags { get; set; } = [];
}
