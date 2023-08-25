using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mvc.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceForItemInReservationPart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PriceForItem",
                table: "ReservationParts",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceForItem",
                table: "ReservationParts");
        }
    }
}
