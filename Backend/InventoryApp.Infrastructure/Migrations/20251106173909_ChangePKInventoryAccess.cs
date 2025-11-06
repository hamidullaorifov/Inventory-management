using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApp.Infrastructure.Migrations;

/// <inheritdoc />
public partial class ChangePKInventoryAccess : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "PK_InventoryAccesses",
            table: "InventoryAccesses");

        migrationBuilder.DropIndex(
            name: "IX_InventoryAccesses_InventoryId",
            table: "InventoryAccesses");

        migrationBuilder.DropColumn(
            name: "Id",
            table: "InventoryAccesses");

        migrationBuilder.DropColumn(
            name: "CreatedAt",
            table: "InventoryAccesses");

        migrationBuilder.DropColumn(
            name: "UpdatedAt",
            table: "InventoryAccesses");

        migrationBuilder.AddPrimaryKey(
            name: "PK_InventoryAccesses",
            table: "InventoryAccesses",
            columns: ["InventoryId", "UserId"]);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "PK_InventoryAccesses",
            table: "InventoryAccesses");

        migrationBuilder.AddColumn<Guid>(
            name: "Id",
            table: "InventoryAccesses",
            type: "uuid",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedAt",
            table: "InventoryAccesses",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedAt",
            table: "InventoryAccesses",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddPrimaryKey(
            name: "PK_InventoryAccesses",
            table: "InventoryAccesses",
            column: "Id");

        migrationBuilder.CreateIndex(
            name: "IX_InventoryAccesses_InventoryId",
            table: "InventoryAccesses",
            column: "InventoryId");
    }
}
