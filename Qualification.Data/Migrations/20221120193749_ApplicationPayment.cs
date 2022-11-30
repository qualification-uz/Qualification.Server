using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class ApplicationPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ApplicationId",
                table: "PaymentRequests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_ApplicationId",
                table: "PaymentRequests",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRequests_Applications_ApplicationId",
                table: "PaymentRequests",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRequests_Applications_ApplicationId",
                table: "PaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRequests_ApplicationId",
                table: "PaymentRequests");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "PaymentRequests");
        }
    }
}
