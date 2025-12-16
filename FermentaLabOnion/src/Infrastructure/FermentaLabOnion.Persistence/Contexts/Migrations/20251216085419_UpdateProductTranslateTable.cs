using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FermentaLabOnion.Persistence.Contexts.Migrations
{
    public partial class UpdateProductTranslateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductTranslates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTranslates_ProductId",
                table: "ProductTranslates",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTranslates_Products_ProductId",
                table: "ProductTranslates",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTranslates_Products_ProductId",
                table: "ProductTranslates");

            migrationBuilder.DropIndex(
                name: "IX_ProductTranslates_ProductId",
                table: "ProductTranslates");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductTranslates");
        }
    }
}
