using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class AssetMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "0034b064-867f-423a-bc37-63de959da0d6");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "44e6b111-0425-40f2-80fe-4d227c4f4232");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "751ca030-213c-4937-80ee-b58a9c8b90bb");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "90d241d5-168e-48fa-a6e2-13ed78c79620");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "cfdcc40c-573e-4ece-abb3-1ad3d1caf713");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "d65c1eab-b444-4665-92f8-e4db2861368b");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01798f81-a231-4f4d-ba8a-315c9ada9b53", "e5a083ef-866b-4e49-8034-825bb64f2b95", "Admin", "ADMIN" },
                    { "33c0b79f-0293-4b6c-ba49-4a0422a94880", "6814fa5f-69bd-43e5-a5f8-394c557249c7", "Teacher", "TEACHER" },
                    { "7b8b55b1-5dbc-406d-b470-f96ad96eec38", "3ffcce8c-03bc-4fa3-b8a5-b463bd09b839", "School", "SCHOOL" },
                    { "eb16cc8b-b47c-42d7-938d-f706aecdcc03", "92b6b286-208f-4936-9586-f2b10240a6ab", "SuperAdmin", "SUPERADMIN" },
                    { "f9635d20-5253-4b0d-8471-ca56327020b5", "75d4f432-243f-4b29-8662-ac71187ffc83", "Tester", "TESTER" },
                    { "fdc1aafb-b3a9-46ce-9774-b825b6f76d9c", "524867c3-26a4-4d06-af47-d2dd8c909f45", "Student", "STUDENT" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "01798f81-a231-4f4d-ba8a-315c9ada9b53");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "33c0b79f-0293-4b6c-ba49-4a0422a94880");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "7b8b55b1-5dbc-406d-b470-f96ad96eec38");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "eb16cc8b-b47c-42d7-938d-f706aecdcc03");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "f9635d20-5253-4b0d-8471-ca56327020b5");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "fdc1aafb-b3a9-46ce-9774-b825b6f76d9c");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0034b064-867f-423a-bc37-63de959da0d6", "bce36d56-cf50-42e8-9643-6d34882af4d8", "Student", "STUDENT" },
                    { "44e6b111-0425-40f2-80fe-4d227c4f4232", "8c0f2b6f-e7ac-4b25-a034-67eabde0df2f", "Admin", "ADMIN" },
                    { "751ca030-213c-4937-80ee-b58a9c8b90bb", "1659379a-31c5-477d-ac79-943cec5ea6d4", "SuperAdmin", "SUPERADMIN" },
                    { "90d241d5-168e-48fa-a6e2-13ed78c79620", "21b25b09-fa64-4357-a942-2d4cfa8adf57", "Teacher", "TEACHER" },
                    { "cfdcc40c-573e-4ece-abb3-1ad3d1caf713", "8d81a2ce-87b4-48bf-ae7c-6d8da99cf76f", "Tester", "TESTER" },
                    { "d65c1eab-b444-4665-92f8-e4db2861368b", "6ab2a2a1-d5c9-43a5-861c-cde0a9358bbe", "School", "SCHOOL" }
                });
        }
    }
}
