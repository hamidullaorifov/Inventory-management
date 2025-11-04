namespace InventoryApp.Application.DTOs.Item;
public class CreateItemDto
{
    public string? CustomId { get; set; }
    public List<FieldValueDto> FieldValues { get; set; } = [];
}
