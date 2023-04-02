using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class SubmissionResultMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "26ee8ea9-819e-4df4-aa3f-aac467fc1830");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "2f0e87a2-2802-47fa-96ce-e92b070f5b38");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "3133b231-b46e-4b5a-ac34-086c25d6f279");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "85cb9a61-3837-400f-9882-2850a8ad0b11");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "9408034b-deea-4fd9-9229-2d60dffa9606");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "d4979c30-e66f-4c12-9f5a-5037eece1861");

            migrationBuilder.CreateTable(
                name: "SubmissionResults",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuizId = table.Column<long>(type: "bigint", nullable: false),
                    QuizQuestionId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    QuestionOptionId = table.Column<long>(type: "bigint", nullable: false),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
                    IsForStudent = table.Column<bool>(type: "boolean", nullable: false),
                    StudentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionResults", x => x.Id);
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubmissionResults");

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

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "26ee8ea9-819e-4df4-aa3f-aac467fc1830", "2cf8b62b-debd-44b5-92b4-159f99188dba", "Teacher", "TEACHER" },
                    { "2f0e87a2-2802-47fa-96ce-e92b070f5b38", "aec68706-ad92-4bb9-affd-36844101277f", "SuperAdmin", "SUPERADMIN" },
                    { "3133b231-b46e-4b5a-ac34-086c25d6f279", "bcab9e7a-c2fa-496f-bf9f-8fb9793398e7", "School", "SCHOOL" },
                    { "85cb9a61-3837-400f-9882-2850a8ad0b11", "cb7ee62c-212c-4cf2-8260-3535cdde67a3", "Tester", "TESTER" },
                    { "9408034b-deea-4fd9-9229-2d60dffa9606", "b81da4ac-2685-4a37-b3a9-9d36841673e7", "Student", "STUDENT" },
                    { "d4979c30-e66f-4c12-9f5a-5037eece1861", "3d5e8e2a-7660-4174-bbb3-20aae4d4b24c", "Admin", "ADMIN" }
                });
        }
    }
}
