using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class schoolid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AspNetUsers_TeacherId",
                table: "Applications");

            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AspNetUsers_TeacherId",
                table: "Applications",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AspNetUsers_TeacherId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AspNetUsers_TeacherId",
                table: "Applications",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
