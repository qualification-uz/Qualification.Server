using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class AddedQuizForStudentIdToQuizResultMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_AspNetUsers_UserId",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Quizes_QuizId",
                table: "Results");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "167eedca-9d7b-4191-baee-faee33a11876");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "370f10d2-ad6d-4a66-b737-74bef692bd4a");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "9add5c11-2dbe-4e16-8dcd-33dffef9d982");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "9f23b979-a55f-4773-b696-ec8d37a175ee");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "b7e385dc-0c98-4fe4-8939-b4b0fb029d54");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "d07c51f7-29a8-46ce-b760-3a6c34ba9071");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Results",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "QuizId",
                table: "Results",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "QuizForStudentId",
                table: "Results",
                type: "bigint",
                nullable: true);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3fa7fa05-8de0-4c3c-902e-7a776fc119f0", "09de12bf-d918-4e49-93e1-5cb4df4009cc", "SuperAdmin", "SUPERADMIN" },
                    { "7cbf0996-31a0-40c3-8b10-adb1559c1d22", "b60bd8d5-98d9-45d0-ba66-a803339264c4", "School", "SCHOOL" },
                    { "86f05919-eb42-4151-9b5e-0877020dd3e6", "780c2c22-6abb-490b-a059-61e726ce101b", "Student", "STUDENT" },
                    { "a518a28b-308e-4c30-9b30-3e634dd8069f", "40169183-5de5-4d89-858a-0264416f8d17", "Teacher", "TEACHER" },
                    { "e34fb60e-d759-4608-844d-f00156894b1c", "b20cadd4-ca46-4a31-ae14-5fc6eed2252a", "Tester", "TESTER" },
                    { "eb91e2e8-bd67-4ee1-9cf4-0d3bb65a1933", "58051729-6d42-480a-99fe-b807e9410220", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Results_QuizForStudentId",
                table: "Results",
                column: "QuizForStudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_AspNetUsers_UserId",
                table: "Results",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Quizes_QuizId",
                table: "Results",
                column: "QuizId",
                principalTable: "Quizes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_QuizForStudents_QuizForStudentId",
                table: "Results",
                column: "QuizForStudentId",
                principalTable: "QuizForStudents",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_AspNetUsers_UserId",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Quizes_QuizId",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_QuizForStudents_QuizForStudentId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_QuizForStudentId",
                table: "Results");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "3fa7fa05-8de0-4c3c-902e-7a776fc119f0");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "7cbf0996-31a0-40c3-8b10-adb1559c1d22");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "86f05919-eb42-4151-9b5e-0877020dd3e6");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a518a28b-308e-4c30-9b30-3e634dd8069f");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "e34fb60e-d759-4608-844d-f00156894b1c");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "eb91e2e8-bd67-4ee1-9cf4-0d3bb65a1933");

            migrationBuilder.DropColumn(
                name: "QuizForStudentId",
                table: "Results");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Results",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "QuizId",
                table: "Results",
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
                    { "167eedca-9d7b-4191-baee-faee33a11876", "03c86664-55fd-4655-9525-fd1b38d22e1a", "Admin", "ADMIN" },
                    { "370f10d2-ad6d-4a66-b737-74bef692bd4a", "cec55ab8-ff9d-472b-ac38-551bddfce028", "Tester", "TESTER" },
                    { "9add5c11-2dbe-4e16-8dcd-33dffef9d982", "46fb37b5-ab57-49e8-82c5-1f6b3df7ff7a", "SuperAdmin", "SUPERADMIN" },
                    { "9f23b979-a55f-4773-b696-ec8d37a175ee", "16757af3-b966-4e0a-ad99-b4325c97c1c5", "Student", "STUDENT" },
                    { "b7e385dc-0c98-4fe4-8939-b4b0fb029d54", "cff7320f-3059-4471-a318-79995d5369fd", "Teacher", "TEACHER" },
                    { "d07c51f7-29a8-46ce-b760-3a6c34ba9071", "2afc3862-bab5-4d54-9733-86a3cb01cf40", "School", "SCHOOL" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Results_AspNetUsers_UserId",
                table: "Results",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Quizes_QuizId",
                table: "Results",
                column: "QuizId",
                principalTable: "Quizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
