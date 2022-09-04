using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class AssetEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssetUrl",
                table: "QuestionAssets");

            migrationBuilder.DropColumn(
                name: "AssetUrl",
                table: "QuestionAnswerAssets");

            migrationBuilder.DropColumn(
                name: "AssetUrl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DocumentUrl",
                table: "Applications");

            migrationBuilder.AddColumn<long>(
                name: "AssetId",
                table: "QuestionAssets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "AssetId",
                table: "QuestionAnswerAssets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "AssetId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DocumentId",
                table: "Applications",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "QuestionAssets");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "QuestionAnswerAssets");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Applications");

            migrationBuilder.AddColumn<string>(
                name: "AssetUrl",
                table: "QuestionAssets",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssetUrl",
                table: "QuestionAnswerAssets",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssetUrl",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentUrl",
                table: "Applications",
                type: "text",
                nullable: true);
        }
    }
}
