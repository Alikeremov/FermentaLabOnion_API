using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FermentaLabOnion.Persistence.Contexts.Migrations
{
    public partial class ShareSpecialsTableWasUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subtittle",
                table: "ShareSpecialTranslates",
                newName: "Subtitle");

            migrationBuilder.RenameColumn(
                name: "Subtittle",
                table: "ShareSpecials",
                newName: "Subtitle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subtitle",
                table: "ShareSpecialTranslates",
                newName: "Subtittle");

            migrationBuilder.RenameColumn(
                name: "Subtitle",
                table: "ShareSpecials",
                newName: "Subtittle");
        }
    }
}
