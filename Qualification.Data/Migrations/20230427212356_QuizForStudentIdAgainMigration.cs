using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class QuizForStudentIdAgainMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
