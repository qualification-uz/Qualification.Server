using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class AddedQuizForStudentToSubmissionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<long>(
                name: "QuizId",
                table: "Submissions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "431ce859-faed-4218-a5e0-60a04fa7231f", "8d1b633b-56cb-46a1-abc4-e9bfbdc4261f", "School", "SCHOOL" },
                    { "5d9909d3-de62-4b9d-b57f-c61683f81675", "ef7d97cf-c47b-4c06-9daf-1cedd2c104ec", "Teacher", "TEACHER" },
                    { "68e6baf8-88e7-471a-9e2f-cff83d32faa6", "d4011f8d-3fb9-41b9-b0e7-dec24781bf0d", "Admin", "ADMIN" },
                    { "88c3dd22-0b4d-43d2-9da3-d0db747a3278", "33ae7f90-5608-49a9-8a57-accd08b6886f", "SuperAdmin", "SUPERADMIN" },
                    { "acfbb464-30a8-456a-a485-1a0739c553a0", "fb17a795-75c6-4531-a27a-00e8a8b5b5a6", "Tester", "TESTER" },
                    { "f3ad250a-ad0a-4300-9ebd-9da2fbf76e72", "a240ba07-5698-42e2-96ca-a63e08133ef5", "Student", "STUDENT" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "431ce859-faed-4218-a5e0-60a04fa7231f");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "5d9909d3-de62-4b9d-b57f-c61683f81675");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "68e6baf8-88e7-471a-9e2f-cff83d32faa6");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "88c3dd22-0b4d-43d2-9da3-d0db747a3278");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "acfbb464-30a8-456a-a485-1a0739c553a0");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "f3ad250a-ad0a-4300-9ebd-9da2fbf76e72");

            migrationBuilder.AlterColumn<long>(
                name: "QuizId",
                table: "Submissions",
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
                    { "3fa7fa05-8de0-4c3c-902e-7a776fc119f0", "09de12bf-d918-4e49-93e1-5cb4df4009cc", "SuperAdmin", "SUPERADMIN" },
                    { "7cbf0996-31a0-40c3-8b10-adb1559c1d22", "b60bd8d5-98d9-45d0-ba66-a803339264c4", "School", "SCHOOL" },
                    { "86f05919-eb42-4151-9b5e-0877020dd3e6", "780c2c22-6abb-490b-a059-61e726ce101b", "Student", "STUDENT" },
                    { "a518a28b-308e-4c30-9b30-3e634dd8069f", "40169183-5de5-4d89-858a-0264416f8d17", "Teacher", "TEACHER" },
                    { "e34fb60e-d759-4608-844d-f00156894b1c", "b20cadd4-ca46-4a31-ae14-5fc6eed2252a", "Tester", "TESTER" },
                    { "eb91e2e8-bd67-4ee1-9cf4-0d3bb65a1933", "58051729-6d42-480a-99fe-b807e9410220", "Admin", "ADMIN" }
                });
        }
    }
}
