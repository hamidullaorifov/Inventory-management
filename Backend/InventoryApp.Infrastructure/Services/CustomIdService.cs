using System.Text.Json;
using InventoryApp.Application.Interfaces.Services;
using InventoryApp.Domain.Entities;
using InventoryApp.Domain.Enums;
using InventoryApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Infrastructure.Services;
public class FixedTextSettings
{
    public string Value { get; set; } = string.Empty;
}
public class CustomIdService(AppDbContext context) : ICustomIdService
{

    public async Task<string> GenerateCustomIdAsync(Inventory inventory)
    {
        var parts = new List<string>();
        foreach (var element in inventory.CustomIdElements.OrderBy(e => e.Order))
        {
            parts.Add(await GeneratePartAsync(inventory, element));
        }

        var customId = string.Join("", parts);

        // enforce uniqueness
        var exists = await context.Items
            .AnyAsync(i => i.InventoryId == inventory.Id && i.CustomId == customId);

        if (exists)
        {
            // retry if it’s random-based; otherwise throw
            throw new InvalidOperationException($"Duplicate CustomId '{customId}' detected.");
        }

        return customId;
    }

    private async Task<string> GeneratePartAsync(Inventory inventory, CustomIdElement element)
    {
        return element.Type switch
        {
            CustomIdElementType.FixedText => JsonSerializer.Deserialize<FixedTextSettings>(element.SettingsJson!)?.Value ?? "",
            CustomIdElementType.Random20Bit => GenerateRandomBits(20),
            CustomIdElementType.Random32Bit => GenerateRandomBits(32),
            CustomIdElementType.Random6Digits => RandomNumberString(6),
            CustomIdElementType.Random9Digits => RandomNumberString(9),
            CustomIdElementType.GuidValue => Guid.NewGuid().ToString("N"),
            CustomIdElementType.DateTime => DateTime.UtcNow.ToString("yyyyMMddHHmmss"),
            CustomIdElementType.Sequence => (await GetNextSequenceAsync(inventory)).ToString(),
            _ => throw new NotSupportedException($"Unsupported element type {element.Type}")
        };
    }

    private async Task<int> GetNextSequenceAsync(Inventory inventory)
    {
        var lastItemSquenceNumber = await context.Items
            .Where(i => i.InventoryId == inventory.Id && i.SequenceNumber.HasValue)
            .OrderByDescending(i => i.CreatedAt)
            .Select(i => i.SequenceNumber)
            .FirstOrDefaultAsync();

        // You could keep a separate "sequence" table for better performance
        return lastItemSquenceNumber == default ? 1 : lastItemSquenceNumber.Value + 1;
    }

    private static string RandomNumberString(int digits)
    {
        var max = (int)Math.Pow(10, digits) - 1;
        var rnd = Random.Shared.Next(0, max);
        return rnd.ToString($"D{digits}");
    }

    private static string GenerateRandomBits(int bits)
    {
        var bytes = new byte[bits / 8];
        Random.Shared.NextBytes(bytes);
        return Convert.ToHexStringLower(bytes);
    }

    public bool ValidateCustomIdFormat(Inventory inventory, string customId)
    {
        // Optionally validate against format (regex or pattern-based)
        return !string.IsNullOrWhiteSpace(customId);
    }
}
