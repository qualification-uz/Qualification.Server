using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class TeacherEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AspNetUsers_TeacherId",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Groups",
                newName: "SchoolYear");

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "Groups",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                table: "Groups",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GradeLetter",
                table: "Groups",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GradeLetterId",
                table: "Groups",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SchoolYearId",
                table: "Groups",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AspNetUsers_TeacherId",
                table: "Applications",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AspNetUsers_TeacherId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "GradeLetter",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "GradeLetterId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "SchoolYearId",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "SchoolYear",
                table: "Groups",
                newName: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AspNetUsers_TeacherId",
                table: "Applications",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
