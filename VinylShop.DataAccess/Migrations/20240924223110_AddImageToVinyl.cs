using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VinylShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddImageToVinyl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Vinyls",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Vinyls");
        }
    }
}
