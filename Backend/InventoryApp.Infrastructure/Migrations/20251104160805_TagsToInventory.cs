using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApp.Infrastructure.Migrations;

/// <inheritdoc />
public partial class TagsToInventory : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "CustomIdFormatJson",
            table: "Inventories",
            type: "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<byte[]>(
            name: "RowVersion",
            table: "Inventories",
            type: "bytea",
            rowVersion: true,
            nullable: false,
            defaultValue: Array.Empty<byte>());

        migrationBuilder.CreateTable(
            name: "InventoryTag",
            columns: table => new
            {
                InventoryId = table.Column<Guid>(type: "uuid", nullable: false),
                TagsName = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_InventoryTag", x => new { x.InventoryId, x.TagsName });
                table.ForeignKey(
                    name: "FK_InventoryTag_Inventories_InventoryId",
                    column: x => x.InventoryId,
                    principalTable: "Inventories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_InventoryTag_Tags_TagsName",
                    column: x => x.TagsName,
                    principalTable: "Tags",
                    principalColumn: "Name",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_InventoryTag_TagsName",
            table: "InventoryTag",
            column: "TagsName");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "InventoryTag");

        migrationBuilder.DropColumn(
            name: "CustomIdFormatJson",
            table: "Inventories");

        migrationBuilder.DropColumn(
            name: "RowVersion",
            table: "Inventories");
    }
}
