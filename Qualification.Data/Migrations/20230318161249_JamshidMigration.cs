using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class JamshidMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_AspNetUsers_UserId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Student_StudentId",
                table: "Submissions");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "0113e77c-0055-4ff8-8f3a-6107f145a720");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "381a168f-1e33-486a-bfa9-44e6dbf10f20");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "44d4c4bb-b070-45fc-aa80-ae4da3457376");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "48cea509-69f4-4ffd-a6eb-004aaabe9c3f");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a6326edd-3898-4466-8cc2-a4500e1c66c7");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "c4ed67cc-e86d-47d5-8dea-4730a1182d32");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Submissions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "StudentId",
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
                    { "26ee8ea9-819e-4df4-aa3f-aac467fc1830", "2cf8b62b-debd-44b5-92b4-159f99188dba", "Teacher", "TEACHER" },
                    { "2f0e87a2-2802-47fa-96ce-e92b070f5b38", "aec68706-ad92-4bb9-affd-36844101277f", "SuperAdmin", "SUPERADMIN" },
                    { "3133b231-b46e-4b5a-ac34-086c25d6f279", "bcab9e7a-c2fa-496f-bf9f-8fb9793398e7", "School", "SCHOOL" },
                    { "85cb9a61-3837-400f-9882-2850a8ad0b11", "cb7ee62c-212c-4cf2-8260-3535cdde67a3", "Tester", "TESTER" },
                    { "9408034b-deea-4fd9-9229-2d60dffa9606", "b81da4ac-2685-4a37-b3a9-9d36841673e7", "Student", "STUDENT" },
                    { "d4979c30-e66f-4c12-9f5a-5037eece1861", "3d5e8e2a-7660-4174-bbb3-20aae4d4b24c", "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_AspNetUsers_UserId",
                table: "Submissions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Student_StudentId",
                table: "Submissions",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_AspNetUsers_UserId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Student_StudentId",
                table: "Submissions");

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

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Submissions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "StudentId",
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
                    { "0113e77c-0055-4ff8-8f3a-6107f145a720", "5761f488-c0e6-4c41-8291-8664c4b2009d", "School", "SCHOOL" },
                    { "381a168f-1e33-486a-bfa9-44e6dbf10f20", "212ce08a-2e6b-4a22-88d3-d88821d5ef01", "Admin", "ADMIN" },
                    { "44d4c4bb-b070-45fc-aa80-ae4da3457376", "5de96a1c-781d-4417-bf31-c5227a9d8f8f", "Tester", "TESTER" },
                    { "48cea509-69f4-4ffd-a6eb-004aaabe9c3f", "fd0fd71d-322f-4804-9ace-5ac40b2e7e5b", "Student", "STUDENT" },
                    { "a6326edd-3898-4466-8cc2-a4500e1c66c7", "0a4bb00f-5543-4929-ab3a-a0969873a864", "SuperAdmin", "SUPERADMIN" },
                    { "c4ed67cc-e86d-47d5-8dea-4730a1182d32", "dd7c5d81-2a72-491f-afc5-f233de5d4ddb", "Teacher", "TEACHER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_AspNetUsers_UserId",
                table: "Submissions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Student_StudentId",
                table: "Submissions",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
