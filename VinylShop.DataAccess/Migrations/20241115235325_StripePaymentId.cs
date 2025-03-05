using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinylShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class StripePaymentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StripePaymentId",
                table: "Payments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StripePaymentId",
                table: "Payments");
        }
    }
}
