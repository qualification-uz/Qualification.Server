using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class AddedQuizForStudentToSubmissionResultmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                table: "SubmissionResults",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "QuizForStudentId",
                table: "SubmissionResults",
                type: "bigint",
                nullable: true);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "40ab0aae-e960-4459-bbdb-0ec897309cf5", "2909d62d-b883-4a01-9e3a-c5d1e9640a05", "Teacher", "TEACHER" },
                    { "43e9632a-5929-4369-8605-2f944c14ac30", "5d57fa1f-46c8-49c6-8a2f-a8d5050d0b0b", "Student", "STUDENT" },
                    { "7bd3b87e-3d3c-49d3-a2f0-601614f76039", "35810808-ace0-4c31-8377-e0f762b04b20", "SuperAdmin", "SUPERADMIN" },
                    { "7d8ae194-6edf-44d3-b4ad-768813bdf94b", "cec9fd7f-fc18-4397-b167-64ea98dfb519", "School", "SCHOOL" },
                    { "8dd981e7-ee5a-4d29-98b5-f7ef722daca4", "db68f4ea-095e-4ab2-b142-04042c2f2cd4", "Admin", "ADMIN" },
                    { "961313d5-9a52-40ca-af6b-0f940ed42376", "4a9a300a-5fe6-441f-8fcf-59c333f4aeb6", "Tester", "TESTER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "40ab0aae-e960-4459-bbdb-0ec897309cf5");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "43e9632a-5929-4369-8605-2f944c14ac30");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "7bd3b87e-3d3c-49d3-a2f0-601614f76039");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "7d8ae194-6edf-44d3-b4ad-768813bdf94b");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "8dd981e7-ee5a-4d29-98b5-f7ef722daca4");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "961313d5-9a52-40ca-af6b-0f940ed42376");

            migrationBuilder.DropColumn(
                name: "QuizForStudentId",
                table: "SubmissionResults");

            migrationBuilder.AlterColumn<long>(
                name: "QuizId",
                table: "SubmissionResults",
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
                    { "431ce859-faed-4218-a5e0-60a04fa7231f", "8d1b633b-56cb-46a1-abc4-e9bfbdc4261f", "School", "SCHOOL" },
                    { "5d9909d3-de62-4b9d-b57f-c61683f81675", "ef7d97cf-c47b-4c06-9daf-1cedd2c104ec", "Teacher", "TEACHER" },
                    { "68e6baf8-88e7-471a-9e2f-cff83d32faa6", "d4011f8d-3fb9-41b9-b0e7-dec24781bf0d", "Admin", "ADMIN" },
                    { "88c3dd22-0b4d-43d2-9da3-d0db747a3278", "33ae7f90-5608-49a9-8a57-accd08b6886f", "SuperAdmin", "SUPERADMIN" },
                    { "acfbb464-30a8-456a-a485-1a0739c553a0", "fb17a795-75c6-4531-a27a-00e8a8b5b5a6", "Tester", "TESTER" },
                    { "f3ad250a-ad0a-4300-9ebd-9da2fbf76e72", "a240ba07-5698-42e2-96ca-a63e08133ef5", "Student", "STUDENT" }
                });
        }
    }
}
