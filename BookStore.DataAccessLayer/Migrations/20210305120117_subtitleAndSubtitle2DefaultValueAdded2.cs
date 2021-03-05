using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.DataAccessLayer.Migrations
{
    public partial class subtitleAndSubtitle2DefaultValueAdded2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d38eb67-48cc-4a89-a305-ecb75397fc28");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94f4c4ef-7c6f-4798-b67f-10d846c2d392");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "614a985b-0e28-48a2-a01f-c7d5c48597ed", "e2587379-3628-44f5-91bb-c0194a2f98f1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "614a985b-0e28-48a2-a01f-c7d5c48597ed");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e2587379-3628-44f5-91bb-c0194a2f98f1");

            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "PrintingEditions");

            migrationBuilder.DropColumn(
                name: "SubTitle2",
                table: "PrintingEditions");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b9009f97-da7a-4faa-b06d-f6e95006ba17", "6e3653ce-9e55-46e6-bd29-9afc406ef9a8", "None", "NONE" },
                    { "02d83994-565b-45ef-a582-b8eeafa05f27", "e258fd81-dc3a-4c05-b041-e91e6a219765", "Admin", "ADMIN" },
                    { "d9707646-0c64-4e15-9130-14677dc5774c", "33b2f216-2da2-4646-8a10-31f785c8077d", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0f199dd4-2978-4e98-8a0f-9020ff816eb4", 0, "8863f2e8-62e4-41b0-9527-0ca7d88a0c14", "storeanager45@gmail.com", true, "BookStore", "Administrator", false, null, "STOREANAGER45@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEKXC5FzL0JPJdqIQV+Gvz4zXJXJxBvWlqdWud0NnqaPVFnvDdWh4V1UiQktStXQCPw==", null, false, null, "7a707234-3cb9-4ef1-a4f6-790280dc46fa", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "02d83994-565b-45ef-a582-b8eeafa05f27", "0f199dd4-2978-4e98-8a0f-9020ff816eb4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9009f97-da7a-4faa-b06d-f6e95006ba17");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9707646-0c64-4e15-9130-14677dc5774c");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "02d83994-565b-45ef-a582-b8eeafa05f27", "0f199dd4-2978-4e98-8a0f-9020ff816eb4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02d83994-565b-45ef-a582-b8eeafa05f27");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0f199dd4-2978-4e98-8a0f-9020ff816eb4");

            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "PrintingEditions",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubTitle2",
                table: "PrintingEditions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "94f4c4ef-7c6f-4798-b67f-10d846c2d392", "04dcfdb5-d434-4a15-9d0a-ac7540115617", "None", "NONE" },
                    { "614a985b-0e28-48a2-a01f-c7d5c48597ed", "c7298b9c-091e-420a-83a8-4ecac9e57fe8", "Admin", "ADMIN" },
                    { "0d38eb67-48cc-4a89-a305-ecb75397fc28", "08b3d7d5-2e3a-46cf-a4f7-7e93fdbb2735", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e2587379-3628-44f5-91bb-c0194a2f98f1", 0, "9c7ba1bd-c472-4c04-9aa7-909bb083e4a2", "storeanager45@gmail.com", true, "BookStore", "Administrator", false, null, "STOREANAGER45@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEMyNLqcoOhJdgOpfnHdV/F5BwE6uI9ihu+l2iVgBQOJ7hxhM+yHvbM2UdQVcS6wigA==", null, false, null, "57157112-bae0-4870-b8da-0d4f65c03b65", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "614a985b-0e28-48a2-a01f-c7d5c48597ed", "e2587379-3628-44f5-91bb-c0194a2f98f1" });
        }
    }
}
