using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class QuizForStudentMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "0eaa0845-8c4c-4f91-bd65-b2c1f35b8398");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "520ba188-75f2-4a3f-a631-6801c2e21e00");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "5bdb64ae-73a4-4aef-aa21-3df1bde50996");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "bd6d6a91-09cc-47fd-8fe6-411e356e5b13");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "dda21317-6b93-46b6-9382-4549724feb21");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "e37e0a5e-f74b-4ced-bb2b-0c48042e2a5b");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "0eaa0845-8c4c-4f91-bd65-b2c1f35b8398", "9570b30c-a80b-4018-8988-fb87c3e958af", "Tester", "TESTER" },
                    { "520ba188-75f2-4a3f-a631-6801c2e21e00", "7037333e-c0f9-46ea-885d-482a6c414052", "Student", "STUDENT" },
                    { "5bdb64ae-73a4-4aef-aa21-3df1bde50996", "10da93b4-b7e1-4303-9ab4-11407d99ce5e", "School", "SCHOOL" },
                    { "bd6d6a91-09cc-47fd-8fe6-411e356e5b13", "9db2423e-2de7-4cfc-88f0-d77f9f2d07eb", "Admin", "ADMIN" },
                    { "dda21317-6b93-46b6-9382-4549724feb21", "579ccbf7-b39c-4777-b726-311ca9584fec", "Teacher", "TEACHER" },
                    { "e37e0a5e-f74b-4ced-bb2b-0c48042e2a5b", "f0088da3-576b-4e08-868f-794015355ef7", "SuperAdmin", "SUPERADMIN" }
                });
        }
    }
}
