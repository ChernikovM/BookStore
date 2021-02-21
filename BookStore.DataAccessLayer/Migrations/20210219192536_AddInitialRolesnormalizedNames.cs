using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.DataAccessLayer.Migrations
{
    public partial class AddInitialRolesnormalizedNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1cc22822-1edc-4d91-ab08-eea7f436751c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93c2d48a-a7ec-41dc-8e76-729433b66795");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af57055b-45f0-4421-9feb-d0d4d864eccc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a79dd411-b708-4f25-a5ab-e1515c6f0c18", "d83916ac-d03d-47a4-9de5-fa6df451d36b", "Nan", "NAN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ab99c120-fc00-49eb-9624-584d6d67d32d", "e1bee1d4-e8c3-49fd-a6b6-4acc5613cf93", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "86b3dab1-db14-47ce-aba5-69baeed723c9", "aa0a427b-9fc1-415c-b7d3-4c3ef0cce73d", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86b3dab1-db14-47ce-aba5-69baeed723c9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a79dd411-b708-4f25-a5ab-e1515c6f0c18");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab99c120-fc00-49eb-9624-584d6d67d32d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1cc22822-1edc-4d91-ab08-eea7f436751c", "64130319-2f3d-4319-bdce-69d1160a5f06", "Nan", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "93c2d48a-a7ec-41dc-8e76-729433b66795", "d2fd8c0a-83a6-431b-baa6-65d424c7b8db", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "af57055b-45f0-4421-9feb-d0d4d864eccc", "87c24613-7ca3-4e6d-a4b7-325fbb8c259e", "User", null });
        }
    }
}
