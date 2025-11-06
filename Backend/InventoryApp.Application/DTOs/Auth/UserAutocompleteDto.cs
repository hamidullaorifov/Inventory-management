namespace InventoryApp.Application.DTOs.Auth;
public class UserAutocompleteDto
{
    public Guid Id { get; set;  }
    public string FullName { get; set; } = default!;
    public string Email { get; set; }
}
