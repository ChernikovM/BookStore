using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.DataAccessLayer.Migrations
{
    public partial class subtitleNotNullAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "SubTitle",
                table: "PrintingEditions",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubTitle2",
                table: "PrintingEditions",
                type: "nvarchar(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "SubTitle2",
                table: "PrintingEditions");

            migrationBuilder.AlterColumn<string>(
                name: "SubTitle",
                table: "PrintingEditions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
