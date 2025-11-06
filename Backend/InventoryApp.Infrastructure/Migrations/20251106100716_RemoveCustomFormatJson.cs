using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApp.Infrastructure.Migrations;

/// <inheritdoc />
public partial class RemoveCustomFormatJson : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CustomIdFormatJson",
            table: "Inventories");

        migrationBuilder.AddColumn<int>(
            name: "SequenceNumber",
            table: "Items",
            type: "integer",
            nullable: true);

        migrationBuilder.CreateTable(
            name: "CustomIdElement",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                InventoryId = table.Column<Guid>(type: "uuid", nullable: false),
                Order = table.Column<int>(type: "integer", nullable: false),
                Type = table.Column<int>(type: "integer", nullable: false),
                SettingsJson = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CustomIdElement", x => x.Id);
                table.ForeignKey(
                    name: "FK_CustomIdElement_Inventories_InventoryId",
                    column: x => x.InventoryId,
                    principalTable: "Inventories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_CustomIdElement_InventoryId",
            table: "CustomIdElement",
            column: "InventoryId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "CustomIdElement");

        migrationBuilder.DropColumn(
            name: "SequenceNumber",
            table: "Items");

        migrationBuilder.AddColumn<string>(
            name: "CustomIdFormatJson",
            table: "Inventories",
            type: "text",
            nullable: false,
            defaultValue: "");
    }
}
