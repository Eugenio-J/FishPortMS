using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishPortMS.Server.Migrations
{
    /// <inheritdoc />
    public partial class Removed_ExpenseCategory_in_VendorExpense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorExpenses_ExpenseCategories_ExpenseCategoryId",
                table: "VendorExpenses");

            migrationBuilder.DropIndex(
                name: "IX_VendorExpenses_ExpenseCategoryId",
                table: "VendorExpenses");

            migrationBuilder.DropColumn(
                name: "ExpenseCategoryId",
                table: "VendorExpenses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpenseCategoryId",
                table: "VendorExpenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VendorExpenses_ExpenseCategoryId",
                table: "VendorExpenses",
                column: "ExpenseCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorExpenses_ExpenseCategories_ExpenseCategoryId",
                table: "VendorExpenses",
                column: "ExpenseCategoryId",
                principalTable: "ExpenseCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
