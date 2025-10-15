namespace InventoryApp.Domain.Entities;
public class ItemFieldValue
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ItemId { get; set; }
    public Item Item { get; set; }

    public Guid FieldDefinitionId { get; set; }
    public InventoryFieldDefinition FieldDefinition { get; set; }
    public string StringValue { get; set; }   // for SingleLine / MultiLine / Document link
    public decimal? NumberValue { get; set; } // for numeric fields
    public bool? BoolValue { get; set; }

    // We could also store a JSON blob for other/unexpected types:
    public string RawJson { get; set; }
}
