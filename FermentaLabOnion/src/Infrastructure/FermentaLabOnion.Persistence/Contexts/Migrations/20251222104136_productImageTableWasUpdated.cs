using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FermentaLabOnion.Persistence.Contexts.Migrations
{
    public partial class productImageTableWasUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrimary",
                table: "ProductImages");

            migrationBuilder.AddColumn<int>(
                name: "ImageType",
                table: "ProductImages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageType",
                table: "ProductImages");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimary",
                table: "ProductImages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
