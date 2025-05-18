using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishPortMS.Server.Migrations
{
    /// <inheritdoc />
    public partial class Modified_PettyCash_UserProfileId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserDetailsId",
                table: "PettyCashes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserDetailsId",
                table: "PettyCashes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
