using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airport.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class dot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "LegType", "NextPosibbleLegs", "PauseTime" },
                values: new object[] { 12, 272, 2 });

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CurrentLeg", "NextPosibbleLegs" },
                values: new object[] { 16, 96 });

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 6,
                column: "CurrentLeg",
                value: 32);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "LegType", "NextPosibbleLegs", "PauseTime" },
                values: new object[] { 4, 544, 4 });

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CurrentLeg", "NextPosibbleLegs" },
                values: new object[] { 32, 64 });

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 6,
                column: "CurrentLeg",
                value: 64);
        }
    }
}
