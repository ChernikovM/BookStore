using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.DataAccessLayer.Migrations
{
    public partial class OrderEntityChangedANDPaymentEntityChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Payments_PaymentId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders");

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

            migrationBuilder.AlterColumn<long>(
                name: "TransactionId",
                table: "Payments",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PaymentId",
                table: "Orders",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0ca9c6af-1693-44a6-ae5f-2251794873ee", "f7df5801-7d9b-470d-b99d-5a438086e936", "None", "NONE" },
                    { "42ee4efc-c743-4044-aae0-0135c037bf1a", "6470dbdc-c5f7-4be1-a007-f4be30d65882", "Admin", "ADMIN" },
                    { "d9b3c644-9724-4735-b003-051176040286", "2d4259af-ee35-4337-a3b5-861fc9a76b4f", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "265c9426-7d55-4c5e-bd0f-8cb8f655051a", 0, "60396649-9b15-4efc-8e6a-675b1f7db41a", "storeanager45@gmail.com", true, "BookStore", "Administrator", false, null, "STOREANAGER45@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEA7OgDiEw7EaBrOskLby6xT5NMglD+W3GgNlTyHSEusyR9o2G8cV2bAqsrdur07eBA==", null, false, null, "6530794f-5b40-4d05-bdf2-024a9b94ac3f", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "42ee4efc-c743-4044-aae0-0135c037bf1a", "265c9426-7d55-4c5e-bd0f-8cb8f655051a" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders",
                column: "PaymentId",
                unique: true,
                filter: "[PaymentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Payments_PaymentId",
                table: "Orders",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Payments_PaymentId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ca9c6af-1693-44a6-ae5f-2251794873ee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9b3c644-9724-4735-b003-051176040286");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "42ee4efc-c743-4044-aae0-0135c037bf1a", "265c9426-7d55-4c5e-bd0f-8cb8f655051a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42ee4efc-c743-4044-aae0-0135c037bf1a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "265c9426-7d55-4c5e-bd0f-8cb8f655051a");

            migrationBuilder.AlterColumn<long>(
                name: "TransactionId",
                table: "Payments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PaymentId",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders",
                column: "PaymentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Payments_PaymentId",
                table: "Orders",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
