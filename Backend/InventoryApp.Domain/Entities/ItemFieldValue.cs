namespace InventoryApp.Domain.Entities;
public class ItemFieldValue
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ItemId { get; set; }
    public Item Item { get; set; }

    public Guid FieldDefinitionId { get; set; }
    public InventoryFieldDefinition FieldDefinition { get; set; }
    public string StringValue { get; set; }
    public decimal? NumberValue { get; set; }
    public bool? BoolValue { get; set; }
    public string RawJson { get; set; }
}
