using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.DataAccessLayer.Migrations
{
    public partial class add_subtitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "PrintingEditions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7307bb4e-b105-4b0b-a099-7cfef904aae1", "b5fbced1-5e64-43d0-b6aa-91e241be169a", "Nan", "NAN" },
                    { "a20e2041-905a-4d80-b6ed-f909180f0ce9", "6f559356-2529-4b09-9701-a34dbed400a0", "Admin", "ADMIN" },
                    { "36205bb9-3607-4776-9cf2-90b706b2bb3b", "b59be14e-0430-4bc2-8c00-a602f4bcd269", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c59c514a-0a04-4c1c-b859-e01c169997fe", 0, "fb10f475-31d6-42c3-a03c-42939916d37f", "storeanager45@gmail.com", true, "BookStore", "Administrator", false, null, "STOREANAGER45@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEAVFVtIQDaUMF6Ji0CT+zHVxY2MqCTDdVHtytisVdIMmPEGQjUNcma8RcANTZsR25w==", null, false, null, "45c70519-8161-48bb-a90f-698be707d2e9", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a20e2041-905a-4d80-b6ed-f909180f0ce9", "c59c514a-0a04-4c1c-b859-e01c169997fe" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36205bb9-3607-4776-9cf2-90b706b2bb3b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7307bb4e-b105-4b0b-a099-7cfef904aae1");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a20e2041-905a-4d80-b6ed-f909180f0ce9", "c59c514a-0a04-4c1c-b859-e01c169997fe" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a20e2041-905a-4d80-b6ed-f909180f0ce9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c59c514a-0a04-4c1c-b859-e01c169997fe");

            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "PrintingEditions");

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
    }
}
