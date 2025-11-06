using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApp.Infrastructure.Migrations;

/// <inheritdoc />
public partial class ChangePKItemLike : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "PK_ItemLikes",
            table: "ItemLikes");

        migrationBuilder.DropIndex(
            name: "IX_ItemLikes_ItemId",
            table: "ItemLikes");

        migrationBuilder.DropColumn(
            name: "Id",
            table: "ItemLikes");

        migrationBuilder.AddPrimaryKey(
            name: "PK_ItemLikes",
            table: "ItemLikes",
            columns: ["ItemId", "UserId"]);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "PK_ItemLikes",
            table: "ItemLikes");

        migrationBuilder.AddColumn<Guid>(
            name: "Id",
            table: "ItemLikes",
            type: "uuid",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddPrimaryKey(
            name: "PK_ItemLikes",
            table: "ItemLikes",
            column: "Id");

        migrationBuilder.CreateIndex(
            name: "IX_ItemLikes_ItemId",
            table: "ItemLikes",
            column: "ItemId");
    }
}
