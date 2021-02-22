using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.DataAccessLayer.Migrations
{
    public partial class RefreshTokenAddedToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3cbdf8a3-1894-481a-a532-948048630f3f", "158565e8-b146-4ecf-9ba8-2d5247b2163f", "Nan", "NAN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "802c4edc-993a-49d9-83f4-4991853b0dcd", "3f00b54d-4dfc-4ed9-ada0-c5af4e3439e6", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8e51e4fc-c806-4322-ae9d-c93f983a7b89", "b1d868bc-aedc-4e39-a016-bf428cb311a8", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3cbdf8a3-1894-481a-a532-948048630f3f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "802c4edc-993a-49d9-83f4-4991853b0dcd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8e51e4fc-c806-4322-ae9d-c93f983a7b89");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

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
    }
}
