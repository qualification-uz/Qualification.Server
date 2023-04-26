using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class AddStudentQuizMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizes_AspNetUsers_UserId",
                table: "Quizes");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "284ffdd7-a296-4879-9dcc-9d416e2ae265");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "4aa64b3e-c7e6-47dd-8a61-368121f50fd4");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "568965fe-bc81-4fc5-bed6-a10121c55283");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "6134e20d-08ed-4b22-a2d7-ec910ad8d5f2");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "71f60b55-5736-4764-ab1e-65d199850b0d");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "89bc890a-1811-48a0-867a-d882606d2e50");

            migrationBuilder.AddColumn<long>(
                name: "QuizForStudentId",
                table: "Submissions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "QuizForStudentId",
                table: "QuizQuestions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Quizes",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "QuizForStudents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    QuestionCount = table.Column<int>(type: "integer", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    StartsAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndsAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizForStudents", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2831f5ab-5db5-4b4a-888f-8baff547d8f1", "0a61650f-4557-494b-a64f-c5ab3f6ba488", "Teacher", "TEACHER" },
                    { "2e60d286-53fd-4c3c-9df9-e1baa8817b07", "b7e01347-56f3-411d-af6a-1067ff09009c", "Student", "STUDENT" },
                    { "518fcc16-2d1b-4a64-8851-1810b4590f8c", "5cdae525-ea72-4834-85a3-171ddd76a062", "SuperAdmin", "SUPERADMIN" },
                    { "56bc9738-aa8e-4d31-b4d9-2b9cdcf0519d", "1cb65249-7028-4210-bc13-8b7fa359a27a", "Tester", "TESTER" },
                    { "a57dd5fd-fdb6-44fc-860a-e877eae482a0", "82a95d58-0f59-4fb5-ba97-273ea079a06d", "Admin", "ADMIN" },
                    { "e9f7a9f0-f445-4faa-a72a-c3fdee0ffb5f", "98dad463-66c2-443d-bc51-9fea3e01a916", "School", "SCHOOL" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_QuizForStudentId",
                table: "Submissions",
                column: "QuizForStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestions_QuizForStudentId",
                table: "QuizQuestions",
                column: "QuizForStudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizes_AspNetUsers_UserId",
                table: "Quizes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestions_QuizForStudents_QuizForStudentId",
                table: "QuizQuestions",
                column: "QuizForStudentId",
                principalTable: "QuizForStudents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_QuizForStudents_QuizForStudentId",
                table: "Submissions",
                column: "QuizForStudentId",
                principalTable: "QuizForStudents",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizes_AspNetUsers_UserId",
                table: "Quizes");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestions_QuizForStudents_QuizForStudentId",
                table: "QuizQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_QuizForStudents_QuizForStudentId",
                table: "Submissions");

            migrationBuilder.DropTable(
                name: "QuizForStudents");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_QuizForStudentId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_QuizQuestions_QuizForStudentId",
                table: "QuizQuestions");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "2831f5ab-5db5-4b4a-888f-8baff547d8f1");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "2e60d286-53fd-4c3c-9df9-e1baa8817b07");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "518fcc16-2d1b-4a64-8851-1810b4590f8c");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "56bc9738-aa8e-4d31-b4d9-2b9cdcf0519d");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a57dd5fd-fdb6-44fc-860a-e877eae482a0");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "e9f7a9f0-f445-4faa-a72a-c3fdee0ffb5f");

            migrationBuilder.DropColumn(
                name: "QuizForStudentId",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "QuizForStudentId",
                table: "QuizQuestions");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Quizes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "284ffdd7-a296-4879-9dcc-9d416e2ae265", "bce3d5ea-f68a-4f19-b1b1-d71a3c4e6f8a", "Teacher", "TEACHER" },
                    { "4aa64b3e-c7e6-47dd-8a61-368121f50fd4", "32a470c4-5b6e-4432-a264-b01a0f5560fe", "School", "SCHOOL" },
                    { "568965fe-bc81-4fc5-bed6-a10121c55283", "30fe2387-8d12-4811-b18f-e09218cc6698", "SuperAdmin", "SUPERADMIN" },
                    { "6134e20d-08ed-4b22-a2d7-ec910ad8d5f2", "61661ef8-3a25-44d8-9dfb-f7a76ff5a14c", "Admin", "ADMIN" },
                    { "71f60b55-5736-4764-ab1e-65d199850b0d", "8da675a4-a691-42bf-9bd8-8afd38222dd2", "Tester", "TESTER" },
                    { "89bc890a-1811-48a0-867a-d882606d2e50", "8e972aa2-c636-4798-b68a-4fa67fbe5963", "Student", "STUDENT" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Quizes_AspNetUsers_UserId",
                table: "Quizes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
