using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApp.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddItemFieldValue : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "ItemFieldValues",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                FieldDefinitionId = table.Column<Guid>(type: "uuid", nullable: false),
                StringValue = table.Column<string>(type: "text", nullable: false),
                NumberValue = table.Column<decimal>(type: "numeric", nullable: true),
                BoolValue = table.Column<bool>(type: "boolean", nullable: true),
                RawJson = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ItemFieldValues", x => x.Id);
                table.ForeignKey(
                    name: "FK_ItemFieldValues_InventoryFieldDefinitions_FieldDefinitionId",
                    column: x => x.FieldDefinitionId,
                    principalTable: "InventoryFieldDefinitions",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ItemFieldValues_Items_ItemId",
                    column: x => x.ItemId,
                    principalTable: "Items",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_ItemFieldValues_FieldDefinitionId",
            table: "ItemFieldValues",
            column: "FieldDefinitionId");

        migrationBuilder.CreateIndex(
            name: "IX_ItemFieldValues_ItemId",
            table: "ItemFieldValues",
            column: "ItemId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ItemFieldValues");
    }
}
