using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class Submission_QuestionOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Submissions_QuestionOptionId",
                table: "Submissions");

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

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_QuestionOptionId",
                table: "Submissions",
                column: "QuestionOptionId");
        }
    }
}
