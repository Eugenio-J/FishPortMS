using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishPortMS.Server.Migrations
{
    /// <inheritdoc />
    public partial class Added_VendorExpense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VendorExpenseCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorExpenseCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VendorExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseCategoryId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReceiptId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorExpenses_ExpenseCategories_ExpenseCategoryId",
                        column: x => x.ExpenseCategoryId,
                        principalTable: "ExpenseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorExpenses_Receipts_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptItems_MasterProductId",
                table: "ReceiptItems",
                column: "MasterProductId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorExpenses_ExpenseCategoryId",
                table: "VendorExpenses",
                column: "ExpenseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorExpenses_ReceiptId",
                table: "VendorExpenses",
                column: "ReceiptId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptItems_MasterProducts_MasterProductId",
                table: "ReceiptItems",
                column: "MasterProductId",
                principalTable: "MasterProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptItems_MasterProducts_MasterProductId",
                table: "ReceiptItems");

            migrationBuilder.DropTable(
                name: "VendorExpenseCategories");

            migrationBuilder.DropTable(
                name: "VendorExpenses");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptItems_MasterProductId",
                table: "ReceiptItems");
        }
    }
}
