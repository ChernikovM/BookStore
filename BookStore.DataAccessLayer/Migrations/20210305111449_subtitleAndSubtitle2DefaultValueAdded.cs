using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.DataAccessLayer.Migrations
{
    public partial class subtitleAndSubtitle2DefaultValueAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "96574b55-c048-428e-9e9f-648be49433f7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1861689-4d5f-4cfe-ba54-5390b86e9c28");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "605d9a3d-d2a8-4aef-a306-c1dcbf7a9e09", "96642fb6-380b-47cd-ab17-0bd2beeced35" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "605d9a3d-d2a8-4aef-a306-c1dcbf7a9e09");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "96642fb6-380b-47cd-ab17-0bd2beeced35");

            migrationBuilder.AlterColumn<string>(
                name: "SubTitle2",
                table: "PrintingEditions",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SubTitle",
                table: "PrintingEditions",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "SubTitle2",
                table: "PrintingEditions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "SubTitle",
                table: "PrintingEditions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "96574b55-c048-428e-9e9f-648be49433f7", "2aa202fb-bc0b-4956-97a2-e412298d711b", "None", "NONE" },
                    { "605d9a3d-d2a8-4aef-a306-c1dcbf7a9e09", "283a1a39-283d-44f4-baf1-9a8e7081f4ac", "Admin", "ADMIN" },
                    { "b1861689-4d5f-4cfe-ba54-5390b86e9c28", "032cffc9-c5f3-48c6-973a-1c1a7dd47545", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "96642fb6-380b-47cd-ab17-0bd2beeced35", 0, "54c7c256-247e-4403-b04f-c18a07ecb81c", "storeanager45@gmail.com", true, "BookStore", "Administrator", false, null, "STOREANAGER45@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEDAd+wB7CoFm96noJG3yB16aKmYi0DMPacBjTennnCIU9syeUqTDsO7/XQGXbF8K1g==", null, false, null, "b0d66b99-6839-48b4-a84d-4da8d07b2aae", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "605d9a3d-d2a8-4aef-a306-c1dcbf7a9e09", "96642fb6-380b-47cd-ab17-0bd2beeced35" });
        }
    }
}
