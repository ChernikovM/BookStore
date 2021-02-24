using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.DataAccessLayer.Migrations
{
    public partial class AddedAdminUserInitialData1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[,]
                {
                    { "d867ce84-44f4-4eb5-9944-9b69b67ecbeb", "8e765ff6-7f8a-461e-bab0-839b4c79c695", "Nan", "NAN" },
                    { "ab1e5260-0321-40a8-815b-5dce3cac68aa", "034aa7a6-dd39-4bc4-8a48-7eb9540895a6", "Admin", "ADMIN" },
                    { "09169979-ea3b-43fd-a26e-641aebf49f06", "367eb1a1-369b-4c90-a189-c1ae5041c284", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f8f4fd0c-e1a9-47c5-b707-0180c99a497f", 0, "7f9f7aef-05fe-4c85-9fc5-8667179f809b", "storeanager45@gmail.com", true, "BookStore", "Administrator", false, null, "STOREANAGER45@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEBiTToIlGXCKBBBWyRP2USRJZPykvprvJwlmTKbCWAZr6oDpW18e+lqwMHeGd0xuWA==", null, false, null, "538d8367-676c-4eed-8ecf-851ae3ba8032", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ab1e5260-0321-40a8-815b-5dce3cac68aa", "f8f4fd0c-e1a9-47c5-b707-0180c99a497f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09169979-ea3b-43fd-a26e-641aebf49f06");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d867ce84-44f4-4eb5-9944-9b69b67ecbeb");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ab1e5260-0321-40a8-815b-5dce3cac68aa", "f8f4fd0c-e1a9-47c5-b707-0180c99a497f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab1e5260-0321-40a8-815b-5dce3cac68aa");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f8f4fd0c-e1a9-47c5-b707-0180c99a497f");

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
    }
}
