using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qualification.Data.Migrations
{
    public partial class AssetIdToUrlMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_QuizForStudents_QuizForStudentId",
                table: "Submissions");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "40ab0aae-e960-4459-bbdb-0ec897309cf5");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "43e9632a-5929-4369-8605-2f944c14ac30");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "7bd3b87e-3d3c-49d3-a2f0-601614f76039");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "7d8ae194-6edf-44d3-b4ad-768813bdf94b");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "8dd981e7-ee5a-4d29-98b5-f7ef722daca4");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "961313d5-9a52-40ca-af6b-0f940ed42376");

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

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAssets_AssetId",
                table: "QuestionAssets",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswerAssets_AssetId",
                table: "QuestionAnswerAssets",
                column: "AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswerAssets_Assets_AssetId",
                table: "QuestionAnswerAssets",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAssets_Assets_AssetId",
                table: "QuestionAssets",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_QuizForStudents_QuizForStudentId",
                table: "Submissions",
                column: "QuizForStudentId",
                principalTable: "QuizForStudents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswerAssets_Assets_AssetId",
                table: "QuestionAnswerAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAssets_Assets_AssetId",
                table: "QuestionAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_QuizForStudents_QuizForStudentId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_QuestionAssets_AssetId",
                table: "QuestionAssets");

            migrationBuilder.DropIndex(
                name: "IX_QuestionAnswerAssets_AssetId",
                table: "QuestionAnswerAssets");

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
                    { "40ab0aae-e960-4459-bbdb-0ec897309cf5", "2909d62d-b883-4a01-9e3a-c5d1e9640a05", "Teacher", "TEACHER" },
                    { "43e9632a-5929-4369-8605-2f944c14ac30", "5d57fa1f-46c8-49c6-8a2f-a8d5050d0b0b", "Student", "STUDENT" },
                    { "7bd3b87e-3d3c-49d3-a2f0-601614f76039", "35810808-ace0-4c31-8377-e0f762b04b20", "SuperAdmin", "SUPERADMIN" },
                    { "7d8ae194-6edf-44d3-b4ad-768813bdf94b", "cec9fd7f-fc18-4397-b167-64ea98dfb519", "School", "SCHOOL" },
                    { "8dd981e7-ee5a-4d29-98b5-f7ef722daca4", "db68f4ea-095e-4ab2-b142-04042c2f2cd4", "Admin", "ADMIN" },
                    { "961313d5-9a52-40ca-af6b-0f940ed42376", "4a9a300a-5fe6-441f-8fcf-59c333f4aeb6", "Tester", "TESTER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_QuizForStudents_QuizForStudentId",
                table: "Submissions",
                column: "QuizForStudentId",
                principalTable: "QuizForStudents",
                principalColumn: "Id");
        }
    }
}
