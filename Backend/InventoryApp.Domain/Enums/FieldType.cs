namespace InventoryApp.Domain.Enums;

public enum FieldType
{
    SingleLineText,
    MultiLineText,
    Number,
    DocumentOrImage, // stored as link/url
    Boolean
}