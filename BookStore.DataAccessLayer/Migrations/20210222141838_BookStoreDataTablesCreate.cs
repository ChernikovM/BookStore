using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.DataAccessLayer.Migrations
{
    public partial class BookStoreDataTablesCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "16c81724-2fe0-43ce-8285-8ed96615e64b", "32210a7b-08a1-4480-acfe-be55db06f057", "Nan", "NAN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "108965ad-fad4-495b-80d9-9a3489d3ddf9", "3f75e0a3-1b43-425c-b3df-9bb795d9af03", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "56f53be5-fd1a-42e7-b736-b53627fced77", "f69ba4dd-0700-493e-bb4b-4f5152796f48", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "108965ad-fad4-495b-80d9-9a3489d3ddf9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16c81724-2fe0-43ce-8285-8ed96615e64b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56f53be5-fd1a-42e7-b736-b53627fced77");

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
    }
}
