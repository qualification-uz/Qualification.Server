using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class PaymentMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    AssetId = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ApplicationId",
                table: "Payments",
                column: "ApplicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

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
        }
    }
}
