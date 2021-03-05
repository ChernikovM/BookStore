using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.DataAccessLayer.Migrations
{
    public partial class subtitleAndSubtitle2DefaultValueAdded3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                nullable: false,
                defaultValue: "Empty");

            migrationBuilder.AddColumn<string>(
                name: "SubTitle2",
                table: "PrintingEditions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Empty");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3387aa3a-fe68-4ef5-89ac-03a4aec16e66", "daa30537-6dc1-4b4b-a5dd-bb13f6a91b63", "None", "NONE" },
                    { "aa45727f-8a9f-4047-8040-85ff1c643e33", "93a81d1f-aee4-4495-b232-a4a99ab1cc8e", "Admin", "ADMIN" },
                    { "08fcf284-7f5e-4cb5-86a4-dd8fbb0a2511", "1ee42596-68f4-4ace-b5ec-5b5ac1e9bafb", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "09b59b08-0f99-4db3-a9c3-249eced9ce87", 0, "3310262b-5ab5-4b68-abdd-0dbd7155fe22", "storeanager45@gmail.com", true, "BookStore", "Administrator", false, null, "STOREANAGER45@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAELeLe6p9VfC0dhvW6MuEi4iv8t+SXIA1rggd4hwgP/+/GooeoHSrs23KyFND1MYr8Q==", null, false, null, "90e8fa6e-a578-433e-95fb-93c54070258c", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "aa45727f-8a9f-4047-8040-85ff1c643e33", "09b59b08-0f99-4db3-a9c3-249eced9ce87" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "08fcf284-7f5e-4cb5-86a4-dd8fbb0a2511");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3387aa3a-fe68-4ef5-89ac-03a4aec16e66");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "aa45727f-8a9f-4047-8040-85ff1c643e33", "09b59b08-0f99-4db3-a9c3-249eced9ce87" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa45727f-8a9f-4047-8040-85ff1c643e33");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "09b59b08-0f99-4db3-a9c3-249eced9ce87");

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
    }
}
