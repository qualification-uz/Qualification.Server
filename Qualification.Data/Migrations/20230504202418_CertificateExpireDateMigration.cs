using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class CertificateExpireDateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "1d019aad-b9f9-4854-bbeb-d80d35b86773");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "290faca2-8261-4e8a-9ece-9d2d9095e4dc");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "5c476142-c5c5-40d7-8eaa-2a33ae7817a2");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "6bf21a0f-cfaa-4516-bfc1-a5c7f88583bd");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "6df64026-5c49-484e-aa85-d67625fdf3e9");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "75ea66e8-34ef-4ab0-a011-d37aefe5a732");

            migrationBuilder.RenameColumn(
                name: "DateIssued",
                table: "Certificates",
                newName: "ExpireDate");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "10a238d4-50d8-4432-9156-e012912bcf76", "ddfc975d-aa6b-4980-87ed-3e1d7e10af60", "SuperAdmin", "SUPERADMIN" },
                    { "7bff1254-1110-40c5-9b1e-3b604ea3017a", "990c88ae-15e7-4aed-a78d-1d76fff8d857", "Tester", "TESTER" },
                    { "9dd2200c-431b-4753-be2d-ad232a6f7e2b", "134d4de8-22c1-4d9e-96d5-1bd5fec867b6", "Teacher", "TEACHER" },
                    { "a5e3a84d-8c88-42bf-8606-3fda7be32f0b", "a3f07b3f-8911-4632-97ed-3b68a0d2b1cb", "Admin", "ADMIN" },
                    { "debd68a8-9270-4247-bbf6-1c1bb639009a", "0aeb3006-ba82-4cd9-b25b-3f5ab3664719", "Student", "STUDENT" },
                    { "ffbee226-2089-4356-84fb-5a6e9334ff43", "f75862e4-efff-4e38-8622-08d728d38a0a", "School", "SCHOOL" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "10a238d4-50d8-4432-9156-e012912bcf76");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "7bff1254-1110-40c5-9b1e-3b604ea3017a");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "9dd2200c-431b-4753-be2d-ad232a6f7e2b");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a5e3a84d-8c88-42bf-8606-3fda7be32f0b");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "debd68a8-9270-4247-bbf6-1c1bb639009a");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "ffbee226-2089-4356-84fb-5a6e9334ff43");

            migrationBuilder.RenameColumn(
                name: "ExpireDate",
                table: "Certificates",
                newName: "DateIssued");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1d019aad-b9f9-4854-bbeb-d80d35b86773", "297297c3-143e-4a7d-bb99-ffdb50464ddc", "Teacher", "TEACHER" },
                    { "290faca2-8261-4e8a-9ece-9d2d9095e4dc", "2a21d407-a776-4e59-9a17-02298a145220", "Admin", "ADMIN" },
                    { "5c476142-c5c5-40d7-8eaa-2a33ae7817a2", "3759349f-2252-4b3a-946e-481972200b44", "SuperAdmin", "SUPERADMIN" },
                    { "6bf21a0f-cfaa-4516-bfc1-a5c7f88583bd", "b936d427-9bb5-4c0e-a9f9-b94c4b6a4afe", "School", "SCHOOL" },
                    { "6df64026-5c49-484e-aa85-d67625fdf3e9", "16f6a290-06db-4c8e-85ec-09db33b0e15e", "Student", "STUDENT" },
                    { "75ea66e8-34ef-4ab0-a011-d37aefe5a732", "acb48559-18d0-473e-a23b-04773e5b9958", "Tester", "TESTER" }
                });
        }
    }
}
