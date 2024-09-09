using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractMonthlyClaimsSystem.Migrations
{
    /// <inheritdoc />
    public partial class CsMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClaimStatus",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClaimStatus",
                table: "Claims");
        }
    }
}
