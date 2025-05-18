using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishPortMS.Server.Migrations
{
    /// <inheritdoc />
    public partial class Added_Consignacion_to_Receipt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ConsignacionId",
                table: "Receipts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_ConsignacionId",
                table: "Receipts",
                column: "ConsignacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Consignacions_ConsignacionId",
                table: "Receipts",
                column: "ConsignacionId",
                principalTable: "Consignacions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Consignacions_ConsignacionId",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_ConsignacionId",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "ConsignacionId",
                table: "Receipts");
        }
    }
}
