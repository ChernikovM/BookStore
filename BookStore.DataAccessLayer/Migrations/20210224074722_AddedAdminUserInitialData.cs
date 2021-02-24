using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.DataAccessLayer.Migrations
{
    public partial class AddedAdminUserInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b258fcd-5620-4907-a4ca-4303d3279ca1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69eb03ec-a997-46c4-bd9e-ad8a4eb124e6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce0282d6-5f0d-41ef-8c82-4c1946cc2bbd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "99bf924d-ddad-423f-82ce-576a309dc7dd", "08c4e778-78cc-4d55-a5be-236e9723ef3e", "Nan", "NAN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d6199e53-6da5-42e4-ba19-6c208082c6bb", "cc60441d-2c50-4e80-a6f8-0b71a478c40b", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "325d7537-24a2-4d79-9c64-1de25f83e50f", "c1426ac5-8454-4105-9f0d-08247a28ff2d", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "325d7537-24a2-4d79-9c64-1de25f83e50f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99bf924d-ddad-423f-82ce-576a309dc7dd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6199e53-6da5-42e4-ba19-6c208082c6bb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "69eb03ec-a997-46c4-bd9e-ad8a4eb124e6", "e14d1d02-fe0b-4826-93f7-af59d0b269f4", "Nan", "NAN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ce0282d6-5f0d-41ef-8c82-4c1946cc2bbd", "2f877215-1d99-470e-9321-1f06caa9a93b", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5b258fcd-5620-4907-a4ca-4303d3279ca1", "b4a3563d-75cb-41a6-9934-9e11d763d8ef", "User", "USER" });
        }
    }
}
