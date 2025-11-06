namespace InventoryApp.Application.DTOs.Item;
public record ItemDetailsDto(
    Guid Id,
    string CustomId,
    List<FieldValueDto> FieldValues
);
