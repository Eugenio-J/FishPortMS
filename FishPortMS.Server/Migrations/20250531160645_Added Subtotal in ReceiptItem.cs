using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishPortMS.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedSubtotalinReceiptItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Subtotal",
                table: "ReceiptItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subtotal",
                table: "ReceiptItems");
        }
    }
}
