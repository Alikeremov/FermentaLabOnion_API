using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FermentaLabOnion.Persistence.Contexts.Migrations
{
    public partial class UpdatedWrongColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tittle",
                table: "ShareSpecialTranslates",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Tittle",
                table: "ShareSpecials",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Tittle",
                table: "QuestionTranslates",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Tittle",
                table: "Questions",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Tittle",
                table: "InformationTranslates",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Tittle",
                table: "Informations",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Tittle",
                table: "ChineseWisdomTranslates",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Tittle",
                table: "ChineseWisdoms",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Tittle",
                table: "AppTranslates",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Tittle",
                table: "Apps",
                newName: "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ShareSpecialTranslates",
                newName: "Tittle");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ShareSpecials",
                newName: "Tittle");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "QuestionTranslates",
                newName: "Tittle");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Questions",
                newName: "Tittle");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "InformationTranslates",
                newName: "Tittle");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Informations",
                newName: "Tittle");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ChineseWisdomTranslates",
                newName: "Tittle");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ChineseWisdoms",
                newName: "Tittle");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "AppTranslates",
                newName: "Tittle");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Apps",
                newName: "Tittle");
        }
    }
}
