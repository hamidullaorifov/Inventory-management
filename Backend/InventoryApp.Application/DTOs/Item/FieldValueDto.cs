namespace InventoryApp.Application.DTOs.Item;
public record FieldValueDto
{
    public Guid FieldDefinitionId { get; set; }
    public string? StringValue { get; set; }
    public decimal? NumberValue { get; set; }
    public bool? BoolValue { get; set; }
}
