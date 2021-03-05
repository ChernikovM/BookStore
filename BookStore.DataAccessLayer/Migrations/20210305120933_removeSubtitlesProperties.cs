using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.DataAccessLayer.Migrations
{
    public partial class removeSubtitlesProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "5b15bdf3-e1fa-4d2b-bf72-a1df1cbe3172", "5e3f0e9f-3391-435d-9f86-8053059e93ac", "None", "NONE" },
                    { "05943e17-0426-441e-b1d4-9e1114f4fbf1", "0b8952f9-07ae-428b-8cf6-0f202e8e5757", "Admin", "ADMIN" },
                    { "6fc2ed6d-ab38-4849-895f-2c8d786fc734", "24f7563f-fd1a-4a76-b45c-23f2fbe1d9f0", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "31003137-ef22-461a-96ef-d76a6bd539f8", 0, "a2490e59-0693-420e-b88e-bdab3fa90d3f", "storeanager45@gmail.com", true, "BookStore", "Administrator", false, null, "STOREANAGER45@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEOI42Cw+ZdMwsH/C+omgBWS/54xY2cFOzqCKdyWTIuyvgkx1mRtVftS/HYI2e/6uiw==", null, false, null, "3217ac39-8efd-487b-b1ff-46b77e4166be", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "05943e17-0426-441e-b1d4-9e1114f4fbf1", "31003137-ef22-461a-96ef-d76a6bd539f8" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b15bdf3-e1fa-4d2b-bf72-a1df1cbe3172");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6fc2ed6d-ab38-4849-895f-2c8d786fc734");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "05943e17-0426-441e-b1d4-9e1114f4fbf1", "31003137-ef22-461a-96ef-d76a6bd539f8" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05943e17-0426-441e-b1d4-9e1114f4fbf1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "31003137-ef22-461a-96ef-d76a6bd539f8");

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
    }
}
