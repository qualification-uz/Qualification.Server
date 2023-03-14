using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class StudentQuizSubmissionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "07f2e0d6-cbf3-4479-91e2-e5e45f1da9bd");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "1e1ae2f5-3f12-4339-adb0-c8e85d322162");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "246c3bbb-4e7d-4577-9373-29f25508b98b");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "2a7a669a-8eae-45fc-92d5-fa09d2ced96e");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "4473b991-1c0a-4079-9115-82e99b35f3a3");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "44d4107e-98bc-4818-a12c-60fe4fdaecc1");

            migrationBuilder.AddColumn<bool>(
                name: "IsForStudent",
                table: "Submissions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "StudentId",
                table: "Submissions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Quizes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "693f67e2-6f85-4202-b33e-232ed011d33f", "b91f7ee1-2f0c-4147-90da-9c41f9f54788", "Teacher", "TEACHER" },
                    { "89e1d34f-3dd4-4fa2-b33b-f01d8a416431", "f92d2ad5-205b-4f80-94c5-f7cc3564b6c7", "Student", "STUDENT" },
                    { "a5d1466c-a5ac-49fd-8776-a9d175c9c3f3", "7e99a012-3019-4692-8a70-273bfd965934", "School", "SCHOOL" },
                    { "a75dbfb9-ba0c-4647-8db7-4cb2a2e440ac", "89af5d58-5714-4c4d-9345-fc3a958c2f4f", "Tester", "TESTER" },
                    { "d68d6c12-47ef-4f18-acbb-1dd04b82c01b", "b4b801d4-1362-4e3f-9746-4d1f82d148bd", "Admin", "ADMIN" },
                    { "ecbb4068-5719-444a-92fc-a6fb0cab3e71", "e158efe7-97df-4118-aa83-1321245217f9", "SuperAdmin", "SUPERADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_StudentId",
                table: "Submissions",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Student_StudentId",
                table: "Submissions",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Student_StudentId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_StudentId",
                table: "Submissions");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "693f67e2-6f85-4202-b33e-232ed011d33f");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "89e1d34f-3dd4-4fa2-b33b-f01d8a416431");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a5d1466c-a5ac-49fd-8776-a9d175c9c3f3");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a75dbfb9-ba0c-4647-8db7-4cb2a2e440ac");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "d68d6c12-47ef-4f18-acbb-1dd04b82c01b");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "ecbb4068-5719-444a-92fc-a6fb0cab3e71");

            migrationBuilder.DropColumn(
                name: "IsForStudent",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Quizes");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "07f2e0d6-cbf3-4479-91e2-e5e45f1da9bd", "705515e8-e8f5-4729-8d88-dea737ca063f", "SuperAdmin", "SUPERADMIN" },
                    { "1e1ae2f5-3f12-4339-adb0-c8e85d322162", "7d38f437-a85f-48b8-a034-8c97e3838cb7", "Teacher", "TEACHER" },
                    { "246c3bbb-4e7d-4577-9373-29f25508b98b", "92ab49d9-fd2a-4b95-93f1-130ace73b432", "School", "SCHOOL" },
                    { "2a7a669a-8eae-45fc-92d5-fa09d2ced96e", "ff32f051-e05c-4916-ab4d-b7690df34a4a", "Admin", "ADMIN" },
                    { "4473b991-1c0a-4079-9115-82e99b35f3a3", "ffa7562b-e5af-4633-97d5-f9154d25d07c", "Student", "STUDENT" },
                    { "44d4107e-98bc-4818-a12c-60fe4fdaecc1", "89ccd9de-8425-4ebc-899c-56945ffc54c3", "Tester", "TESTER" }
                });
        }
    }
}
