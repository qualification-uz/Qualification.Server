using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class LastMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "29a648f3-079a-45ad-a702-f9e41faa52c6");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "450c2bdd-0803-4309-9879-91add872edc0");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "4cd128f2-6ce5-48c1-a216-08412d7f14d7");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "6e53d942-0d2a-48db-b738-b45be42273df");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "99cab103-666a-4f74-92fc-60d966ae20a2");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "9a8ad44b-197c-4784-994b-be3ea1349bc0");

            migrationBuilder.AddColumn<long>(
                name: "ERPId",
                table: "Student",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0eaa0845-8c4c-4f91-bd65-b2c1f35b8398", "9570b30c-a80b-4018-8988-fb87c3e958af", "Tester", "TESTER" },
                    { "520ba188-75f2-4a3f-a631-6801c2e21e00", "7037333e-c0f9-46ea-885d-482a6c414052", "Student", "STUDENT" },
                    { "5bdb64ae-73a4-4aef-aa21-3df1bde50996", "10da93b4-b7e1-4303-9ab4-11407d99ce5e", "School", "SCHOOL" },
                    { "bd6d6a91-09cc-47fd-8fe6-411e356e5b13", "9db2423e-2de7-4cfc-88f0-d77f9f2d07eb", "Admin", "ADMIN" },
                    { "dda21317-6b93-46b6-9382-4549724feb21", "579ccbf7-b39c-4777-b726-311ca9584fec", "Teacher", "TEACHER" },
                    { "e37e0a5e-f74b-4ced-bb2b-0c48042e2a5b", "f0088da3-576b-4e08-868f-794015355ef7", "SuperAdmin", "SUPERADMIN" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "0eaa0845-8c4c-4f91-bd65-b2c1f35b8398");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "520ba188-75f2-4a3f-a631-6801c2e21e00");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "5bdb64ae-73a4-4aef-aa21-3df1bde50996");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "bd6d6a91-09cc-47fd-8fe6-411e356e5b13");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "dda21317-6b93-46b6-9382-4549724feb21");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "e37e0a5e-f74b-4ced-bb2b-0c48042e2a5b");

            migrationBuilder.DropColumn(
                name: "ERPId",
                table: "Student");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "29a648f3-079a-45ad-a702-f9e41faa52c6", "6b0e001a-e6dd-49d1-be31-de7e32fb9171", "Tester", "TESTER" },
                    { "450c2bdd-0803-4309-9879-91add872edc0", "3a13e2c4-2954-40cf-970d-0b85567c96c0", "SuperAdmin", "SUPERADMIN" },
                    { "4cd128f2-6ce5-48c1-a216-08412d7f14d7", "1c6a8cd5-b89c-419e-9ad4-495a0d0317d2", "School", "SCHOOL" },
                    { "6e53d942-0d2a-48db-b738-b45be42273df", "858e6587-51d5-4339-9a6d-6f49499db517", "Admin", "ADMIN" },
                    { "99cab103-666a-4f74-92fc-60d966ae20a2", "d29cf17a-df01-44d2-bd3c-113995ee7c5a", "Student", "STUDENT" },
                    { "9a8ad44b-197c-4784-994b-be3ea1349bc0", "9686a62c-7a46-46e2-b4df-de6bce8672b5", "Teacher", "TEACHER" }
                });
        }
    }
}
