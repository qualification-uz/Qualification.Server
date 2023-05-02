using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class RemoveApplicationFromCertificateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_Applications_ApplicationId",
                table: "Certificates");

            migrationBuilder.DropIndex(
                name: "IX_Certificates_ApplicationId",
                table: "Certificates");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "3a28af36-84f1-4b88-a867-3d41d46a231f");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "4a5c8cf9-b2af-4dc3-8fe7-4e507c391028");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "bcd28358-d785-4ceb-9894-d0bc4f28408f");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "befe9317-ce29-47f8-85eb-07f3da6ff287");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "c7df3245-6472-490b-ae6b-cbd1fa756f20");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "f0b77de8-6fba-47f4-9e69-2c82c4a8afda");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3a28af36-84f1-4b88-a867-3d41d46a231f", "bac55340-66f5-4cef-abaf-afbdd9d85d5d", "Teacher", "TEACHER" },
                    { "4a5c8cf9-b2af-4dc3-8fe7-4e507c391028", "a7999429-3834-4606-ad97-cfc5a471c5bf", "SuperAdmin", "SUPERADMIN" },
                    { "bcd28358-d785-4ceb-9894-d0bc4f28408f", "2f1da6d0-d3a0-478b-98ab-8bc7245658de", "Student", "STUDENT" },
                    { "befe9317-ce29-47f8-85eb-07f3da6ff287", "6ad458e1-64ee-4c0c-8595-28f517be1012", "Tester", "TESTER" },
                    { "c7df3245-6472-490b-ae6b-cbd1fa756f20", "7b932ef7-fa0c-4c0e-8ee0-6be540655bb2", "School", "SCHOOL" },
                    { "f0b77de8-6fba-47f4-9e69-2c82c4a8afda", "6e9dfcf4-2780-4055-8342-50a5c9d2678b", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_ApplicationId",
                table: "Certificates",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_Applications_ApplicationId",
                table: "Certificates",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id");
        }
    }
}
