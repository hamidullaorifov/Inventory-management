namespace InventoryApp.Application.Common.Models;
public record PagedResult<T>(IEnumerable<T> Items, int TotalCount);
