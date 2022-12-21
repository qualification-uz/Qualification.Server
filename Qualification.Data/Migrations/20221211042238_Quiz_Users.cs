using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class Quiz_Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "QuestionOptionId",
                table: "Submissions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "QuizQuestionId",
                table: "Submissions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Submissions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Quizes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_QuestionOptionId",
                table: "Submissions",
                column: "QuestionOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_QuizQuestionId",
                table: "Submissions",
                column: "QuizQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_UserId",
                table: "Submissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizes_UserId",
                table: "Quizes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizes_AspNetUsers_UserId",
                table: "Quizes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_AspNetUsers_UserId",
                table: "Submissions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_QuizQuestionOptions_QuestionOptionId",
                table: "Submissions",
                column: "QuestionOptionId",
                principalTable: "QuizQuestionOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_QuizQuestions_QuizQuestionId",
                table: "Submissions",
                column: "QuizQuestionId",
                principalTable: "QuizQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizes_AspNetUsers_UserId",
                table: "Quizes");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_AspNetUsers_UserId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_QuizQuestionOptions_QuestionOptionId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_QuizQuestions_QuizQuestionId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_QuestionOptionId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_QuizQuestionId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_UserId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Quizes_UserId",
                table: "Quizes");

            migrationBuilder.DropColumn(
                name: "QuestionOptionId",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "QuizQuestionId",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Quizes");
        }
    }
}
