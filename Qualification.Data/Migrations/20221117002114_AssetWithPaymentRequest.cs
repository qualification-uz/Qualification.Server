using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class AssetWithPaymentRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PaymentRequestId",
                table: "PaymentAssets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAssets_PaymentRequestId",
                table: "PaymentAssets",
                column: "PaymentRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentAssets_PaymentRequests_PaymentRequestId",
                table: "PaymentAssets",
                column: "PaymentRequestId",
                principalTable: "PaymentRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentAssets_PaymentRequests_PaymentRequestId",
                table: "PaymentAssets");

            migrationBuilder.DropIndex(
                name: "IX_PaymentAssets_PaymentRequestId",
                table: "PaymentAssets");

            migrationBuilder.DropColumn(
                name: "PaymentRequestId",
                table: "PaymentAssets");
        }
    }
}
