using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airport.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class updatePauseTime3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 4,
                column: "PauseTime",
                value: 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 4,
                column: "PauseTime",
                value: 8);
        }
    }
}
