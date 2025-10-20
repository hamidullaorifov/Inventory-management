using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApp.Infrastructure.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "InventoryCategories",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_InventoryCategories", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Tags",
            columns: table => new
            {
                Name = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Tags", x => x.Name);
            });

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Email = table.Column<string>(type: "text", nullable: false),
                FullName = table.Column<string>(type: "text", nullable: false),
                ProfilePictureUrl = table.Column<string>(type: "text", nullable: true),
                IsBlocked = table.Column<bool>(type: "boolean", nullable: false),
                IsAdmin = table.Column<bool>(type: "boolean", nullable: false),
                PasswordHash = table.Column<string>(type: "text", nullable: false),
                Language = table.Column<string>(type: "text", nullable: false),
                Theme = table.Column<string>(type: "text", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Inventories",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "text", nullable: false),
                Description = table.Column<string>(type: "text", nullable: false),
                ImageUrl = table.Column<string>(type: "text", nullable: true),
                IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Inventories", x => x.Id);
                table.ForeignKey(
                    name: "FK_Inventories_InventoryCategories_CategoryId",
                    column: x => x.CategoryId,
                    principalTable: "InventoryCategories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Inventories_Users_OwnerId",
                    column: x => x.OwnerId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "InventoryAccesses",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                InventoryId = table.Column<Guid>(type: "uuid", nullable: false),
                UserId = table.Column<Guid>(type: "uuid", nullable: false),
                CanWrite = table.Column<bool>(type: "boolean", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_InventoryAccesses", x => x.Id);
                table.ForeignKey(
                    name: "FK_InventoryAccesses_Inventories_InventoryId",
                    column: x => x.InventoryId,
                    principalTable: "Inventories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_InventoryAccesses_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "InventoryFieldDefinitions",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                InventoryId = table.Column<Guid>(type: "uuid", nullable: false),
                Type = table.Column<int>(type: "integer", nullable: false),
                Title = table.Column<string>(type: "text", nullable: false),
                Description = table.Column<string>(type: "text", nullable: false),
                ShowInTable = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_InventoryFieldDefinitions", x => x.Id);
                table.ForeignKey(
                    name: "FK_InventoryFieldDefinitions_Inventories_InventoryId",
                    column: x => x.InventoryId,
                    principalTable: "Inventories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Items",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                InventoryId = table.Column<Guid>(type: "uuid", nullable: false),
                CustomId = table.Column<string>(type: "text", nullable: false),
                CreatedById = table.Column<Guid>(type: "uuid", nullable: false),
                UpdatedById = table.Column<Guid>(type: "uuid", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Items", x => x.Id);
                table.ForeignKey(
                    name: "FK_Items_Inventories_InventoryId",
                    column: x => x.InventoryId,
                    principalTable: "Inventories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Items_Users_CreatedById",
                    column: x => x.CreatedById,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Items_Users_UpdatedById",
                    column: x => x.UpdatedById,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ItemLikes",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                UserId = table.Column<Guid>(type: "uuid", nullable: false),
                LikedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ItemLikes", x => x.Id);
                table.ForeignKey(
                    name: "FK_ItemLikes_Items_ItemId",
                    column: x => x.ItemId,
                    principalTable: "Items",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ItemLikes_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Inventories_CategoryId",
            table: "Inventories",
            column: "CategoryId");

        migrationBuilder.CreateIndex(
            name: "IX_Inventories_OwnerId",
            table: "Inventories",
            column: "OwnerId");

        migrationBuilder.CreateIndex(
            name: "IX_InventoryAccesses_InventoryId",
            table: "InventoryAccesses",
            column: "InventoryId");

        migrationBuilder.CreateIndex(
            name: "IX_InventoryAccesses_UserId",
            table: "InventoryAccesses",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_InventoryFieldDefinitions_InventoryId",
            table: "InventoryFieldDefinitions",
            column: "InventoryId");

        migrationBuilder.CreateIndex(
            name: "IX_ItemLikes_ItemId",
            table: "ItemLikes",
            column: "ItemId");

        migrationBuilder.CreateIndex(
            name: "IX_ItemLikes_UserId",
            table: "ItemLikes",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Items_CreatedById",
            table: "Items",
            column: "CreatedById");

        migrationBuilder.CreateIndex(
            name: "IX_Items_InventoryId",
            table: "Items",
            column: "InventoryId");

        migrationBuilder.CreateIndex(
            name: "IX_Items_UpdatedById",
            table: "Items",
            column: "UpdatedById");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "InventoryAccesses");

        migrationBuilder.DropTable(
            name: "InventoryFieldDefinitions");

        migrationBuilder.DropTable(
            name: "ItemLikes");

        migrationBuilder.DropTable(
            name: "Tags");

        migrationBuilder.DropTable(
            name: "Items");

        migrationBuilder.DropTable(
            name: "Inventories");

        migrationBuilder.DropTable(
            name: "InventoryCategories");

        migrationBuilder.DropTable(
            name: "Users");
    }
}
