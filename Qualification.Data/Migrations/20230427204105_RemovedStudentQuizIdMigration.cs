using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class RemovedStudentQuizIdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "0f0112f3-abcd-428c-8b24-a481afd56d8c");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "10358651-a72e-4dd7-8b30-cda0bf7c2963");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "6a8c2795-ed2b-4621-999a-03bfb2ab833e");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "78f56ec9-c59c-4fd5-a435-bd06199a8662");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a2e7b246-eac8-41c1-8bae-2bf88c05192e");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "fb368b5b-862e-4017-848e-6871a1a7d766");

            migrationBuilder.DropColumn(
                name: "StudentQuizId",
                table: "QuizQuestions");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01a80b0e-dfad-4d09-b2ac-eb5461f385db", "8e447913-c579-4ac6-a355-3c3765c4a815", "Student", "STUDENT" },
                    { "25657599-791b-49de-9869-ac5d72eb7cdd", "8ffb0bb9-5454-4a63-b7a2-a41efcb41e3d", "Tester", "TESTER" },
                    { "9b58d582-e120-4742-9739-976870fa7924", "89c366ab-705f-4e4d-ac11-ce684865a686", "Teacher", "TEACHER" },
                    { "c5c86e50-eba7-4111-be57-326804690861", "46a2a83c-1f7e-4fd7-ac7e-a569d50323ec", "School", "SCHOOL" },
                    { "d68b207a-58a0-472e-8732-9dfa112c5cde", "8d49d4ee-09b2-420f-bb87-be7bdc9d916d", "SuperAdmin", "SUPERADMIN" },
                    { "ddf81ec7-20c2-463b-a942-4f4adf18b3cd", "3a0435a8-7fa3-41a3-8c13-1a1948d953ec", "Admin", "ADMIN" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "01a80b0e-dfad-4d09-b2ac-eb5461f385db");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "25657599-791b-49de-9869-ac5d72eb7cdd");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "9b58d582-e120-4742-9739-976870fa7924");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "c5c86e50-eba7-4111-be57-326804690861");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "d68b207a-58a0-472e-8732-9dfa112c5cde");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "ddf81ec7-20c2-463b-a942-4f4adf18b3cd");

            migrationBuilder.AddColumn<long>(
                name: "StudentQuizId",
                table: "QuizQuestions",
                type: "bigint",
                nullable: true);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f0112f3-abcd-428c-8b24-a481afd56d8c", "a281341d-36a2-40f2-8291-2b35d1124524", "Tester", "TESTER" },
                    { "10358651-a72e-4dd7-8b30-cda0bf7c2963", "00aab32e-0539-4f30-98d0-63b2ea05d74e", "School", "SCHOOL" },
                    { "6a8c2795-ed2b-4621-999a-03bfb2ab833e", "45fb4371-f8e8-494a-b106-6c53c2dacfcf", "Admin", "ADMIN" },
                    { "78f56ec9-c59c-4fd5-a435-bd06199a8662", "1f9590f5-cd19-4aa4-a06a-ae7295056635", "Teacher", "TEACHER" },
                    { "a2e7b246-eac8-41c1-8bae-2bf88c05192e", "d8e7122c-06af-4d97-90cd-07482b7ca828", "Student", "STUDENT" },
                    { "fb368b5b-862e-4017-848e-6871a1a7d766", "85d944f5-2870-4cd2-8276-40f0b56da38a", "SuperAdmin", "SUPERADMIN" }
                });
        }
    }
}
