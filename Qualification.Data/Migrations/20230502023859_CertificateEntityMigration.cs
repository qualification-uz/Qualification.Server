using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class CertificateEntityMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    ApplicationId = table.Column<long>(type: "bigint", nullable: true),
                    SubjectScore = table.Column<double>(type: "double precision", nullable: false),
                    PedagogicalScore = table.Column<double>(type: "double precision", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: true),
                    DateIssued = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificates_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id");
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificates");

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
                    { "01798f81-a231-4f4d-ba8a-315c9ada9b53", "e5a083ef-866b-4e49-8034-825bb64f2b95", "Admin", "ADMIN" },
                    { "33c0b79f-0293-4b6c-ba49-4a0422a94880", "6814fa5f-69bd-43e5-a5f8-394c557249c7", "Teacher", "TEACHER" },
                    { "7b8b55b1-5dbc-406d-b470-f96ad96eec38", "3ffcce8c-03bc-4fa3-b8a5-b463bd09b839", "School", "SCHOOL" },
                    { "eb16cc8b-b47c-42d7-938d-f706aecdcc03", "92b6b286-208f-4936-9586-f2b10240a6ab", "SuperAdmin", "SUPERADMIN" },
                    { "f9635d20-5253-4b0d-8471-ca56327020b5", "75d4f432-243f-4b29-8662-ac71187ffc83", "Tester", "TESTER" },
                    { "fdc1aafb-b3a9-46ce-9774-b825b6f76d9c", "524867c3-26a4-4d06-af47-d2dd8c909f45", "Student", "STUDENT" }
                });
        }
    }
}
