using InventoryApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Infrastructure.Persistence;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<InventoryAccess> InventoryAccesses { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<InventoryCategory> InventoryCategories { get; set; }
    public DbSet<InventoryFieldDefinition> InventoryFieldDefinitions { get; set; }
    public DbSet<ItemFieldValue> ItemFieldValues { get; set; }
    public DbSet<ItemLike> ItemLikes { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Tag>()
            .HasKey(t => t.Name);

        modelBuilder.Entity<Inventory>()
            .HasMany(i => i.Fields)
            .WithOne(f => f.Inventory)
            .HasForeignKey(f => f.InventoryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Inventory>()
            .HasMany(i => i.Tags)
            .WithMany();
    }
}
