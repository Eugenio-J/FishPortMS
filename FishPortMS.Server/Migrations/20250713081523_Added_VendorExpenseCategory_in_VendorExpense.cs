using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishPortMS.Server.Migrations
{
    /// <inheritdoc />
    public partial class Added_VendorExpenseCategory_in_VendorExpense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VendorExpenseCategoryId",
                table: "VendorExpenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VendorExpenses_VendorExpenseCategoryId",
                table: "VendorExpenses",
                column: "VendorExpenseCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorExpenses_VendorExpenseCategories_VendorExpenseCategoryId",
                table: "VendorExpenses",
                column: "VendorExpenseCategoryId",
                principalTable: "VendorExpenseCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorExpenses_VendorExpenseCategories_VendorExpenseCategoryId",
                table: "VendorExpenses");

            migrationBuilder.DropIndex(
                name: "IX_VendorExpenses_VendorExpenseCategoryId",
                table: "VendorExpenses");

            migrationBuilder.DropColumn(
                name: "VendorExpenseCategoryId",
                table: "VendorExpenses");
        }
    }
}
