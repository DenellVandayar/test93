using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractMonthlyClaimsSystem.Migrations
{
    /// <inheritdoc />
    public partial class ClaimMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LecturerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimPeriodsStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClaimPeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoursWorked = table.Column<double>(type: "float", nullable: false),
                    RatePerHour = table.Column<double>(type: "float", nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    DescriptionOfWork = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Claims_AspNetUsers_LecturerID",
                        column: x => x.LecturerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Claims_LecturerID",
                table: "Claims",
                column: "LecturerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claims");
        }
    }
}
