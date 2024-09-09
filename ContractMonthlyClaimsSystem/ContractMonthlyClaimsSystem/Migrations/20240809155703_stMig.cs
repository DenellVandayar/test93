using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractMonthlyClaimsSystem.Migrations
{
    /// <inheritdoc />
    public partial class stMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Claims");

            migrationBuilder.AddColumn<string>(
                name: "StatusAcademic",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusProg",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusAcademic",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "StatusProg",
                table: "Claims");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
