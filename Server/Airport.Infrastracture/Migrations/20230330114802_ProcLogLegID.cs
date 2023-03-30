using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airport.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class ProcLogLegID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Leg",
                table: "ProcessLogger",
                newName: "LegId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LegId",
                table: "ProcessLogger",
                newName: "Leg");
        }
    }
}
