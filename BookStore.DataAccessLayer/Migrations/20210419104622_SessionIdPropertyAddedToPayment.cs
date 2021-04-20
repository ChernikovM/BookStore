using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.DataAccessLayer.Migrations
{
    public partial class SessionIdPropertyAddedToPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Payments");

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Payments");

            migrationBuilder.AddColumn<long>(
                name: "TransactionId",
                table: "Payments",
                type: "bigint",
                nullable: true);
        }
    }
}
