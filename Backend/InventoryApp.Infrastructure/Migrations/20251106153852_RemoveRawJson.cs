using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApp.Infrastructure.Migrations;

/// <inheritdoc />
public partial class RemoveRawJson : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "RawJson",
            table: "ItemFieldValues");

        migrationBuilder.AlterColumn<string>(
            name: "StringValue",
            table: "ItemFieldValues",
            type: "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "StringValue",
            table: "ItemFieldValues",
            type: "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AddColumn<string>(
            name: "RawJson",
            table: "ItemFieldValues",
            type: "text",
            nullable: false,
            defaultValue: "");
    }
}
