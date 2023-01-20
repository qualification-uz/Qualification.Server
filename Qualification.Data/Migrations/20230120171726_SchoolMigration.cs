using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class SchoolMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Submissions_QuestionOptionId",
                table: "Submissions");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistrictName",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegionName",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_QuestionOptionId",
                table: "Submissions",
                column: "QuestionOptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestions_QuizId",
                table: "QuizQuestions",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestions_Quizes_QuizId",
                table: "QuizQuestions",
                column: "QuizId",
                principalTable: "Quizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestions_Quizes_QuizId",
                table: "QuizQuestions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_QuestionOptionId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_QuizQuestions_QuizId",
                table: "QuizQuestions");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DistrictName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RegionName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_QuestionOptionId",
                table: "Submissions",
                column: "QuestionOptionId");
        }
    }
}
