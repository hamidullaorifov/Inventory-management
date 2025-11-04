using System.ComponentModel.DataAnnotations;
using InventoryApp.Domain.Enums;

namespace InventoryApp.Application.DTOs.Inventory;
public class InventoryFieldUpdateDto
{
    [EnumDataType(typeof(FieldType))]
    public FieldType Type { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool ShowInTable { get; set; }
}
