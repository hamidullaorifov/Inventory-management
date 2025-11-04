using System.Linq.Expressions;
using InventoryApp.Application.Interfaces.Repositories;
using InventoryApp.Domain.Entities;
using InventoryApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Infrastructure.Repositories;
public class TagRepository(AppDbContext context) : ITagRepository
{
    public async Task AddRangeAsync(IEnumerable<Tag> tags)
    {
        await context.Tags.AddRangeAsync(tags);
    }

    public async Task<List<Tag>> GetAllAsync(Expression<Func<Tag, bool>> predicate)
    {
        return await context.Tags.Where(predicate).ToListAsync();
    }
}
