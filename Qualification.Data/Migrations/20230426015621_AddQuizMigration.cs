using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class AddQuizMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestions_Quizes_QuizId",
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

            migrationBuilder.AlterColumn<long>(
                name: "QuizId",
                table: "QuizQuestions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "29a648f3-079a-45ad-a702-f9e41faa52c6", "6b0e001a-e6dd-49d1-be31-de7e32fb9171", "Tester", "TESTER" },
                    { "450c2bdd-0803-4309-9879-91add872edc0", "3a13e2c4-2954-40cf-970d-0b85567c96c0", "SuperAdmin", "SUPERADMIN" },
                    { "4cd128f2-6ce5-48c1-a216-08412d7f14d7", "1c6a8cd5-b89c-419e-9ad4-495a0d0317d2", "School", "SCHOOL" },
                    { "6e53d942-0d2a-48db-b738-b45be42273df", "858e6587-51d5-4339-9a6d-6f49499db517", "Admin", "ADMIN" },
                    { "99cab103-666a-4f74-92fc-60d966ae20a2", "d29cf17a-df01-44d2-bd3c-113995ee7c5a", "Student", "STUDENT" },
                    { "9a8ad44b-197c-4784-994b-be3ea1349bc0", "9686a62c-7a46-46e2-b4df-de6bce8672b5", "Teacher", "TEACHER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestions_Quizes_QuizId",
                table: "QuizQuestions",
                column: "QuizId",
                principalTable: "Quizes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestions_Quizes_QuizId",
                table: "QuizQuestions");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "29a648f3-079a-45ad-a702-f9e41faa52c6");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "450c2bdd-0803-4309-9879-91add872edc0");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "4cd128f2-6ce5-48c1-a216-08412d7f14d7");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "6e53d942-0d2a-48db-b738-b45be42273df");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "99cab103-666a-4f74-92fc-60d966ae20a2");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "9a8ad44b-197c-4784-994b-be3ea1349bc0");

            migrationBuilder.AlterColumn<long>(
                name: "QuizId",
                table: "QuizQuestions",
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
                    { "2831f5ab-5db5-4b4a-888f-8baff547d8f1", "0a61650f-4557-494b-a64f-c5ab3f6ba488", "Teacher", "TEACHER" },
                    { "2e60d286-53fd-4c3c-9df9-e1baa8817b07", "b7e01347-56f3-411d-af6a-1067ff09009c", "Student", "STUDENT" },
                    { "518fcc16-2d1b-4a64-8851-1810b4590f8c", "5cdae525-ea72-4834-85a3-171ddd76a062", "SuperAdmin", "SUPERADMIN" },
                    { "56bc9738-aa8e-4d31-b4d9-2b9cdcf0519d", "1cb65249-7028-4210-bc13-8b7fa359a27a", "Tester", "TESTER" },
                    { "a57dd5fd-fdb6-44fc-860a-e877eae482a0", "82a95d58-0f59-4fb5-ba97-273ea079a06d", "Admin", "ADMIN" },
                    { "e9f7a9f0-f445-4faa-a72a-c3fdee0ffb5f", "98dad463-66c2-443d-bc51-9fea3e01a916", "School", "SCHOOL" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestions_Quizes_QuizId",
                table: "QuizQuestions",
                column: "QuizId",
                principalTable: "Quizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
