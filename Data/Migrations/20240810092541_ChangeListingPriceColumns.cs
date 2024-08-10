using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auction.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeListingPriceColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPrice",
                table: "Listings");

            migrationBuilder.RenameColumn(
                name: "StartingPrice",
                table: "Listings",
                newName: "Price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Listings",
                newName: "StartingPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentPrice",
                table: "Listings",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
