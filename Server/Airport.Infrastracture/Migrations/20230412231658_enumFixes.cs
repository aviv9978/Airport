using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airport.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class enumFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 2,
                column: "NextPosibbleLegs",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CurrentLeg", "NextPosibbleLegs" },
                values: new object[] { 4, 8 });

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CurrentLeg", "NextPosibbleLegs" },
                values: new object[] { 8, 272 });

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
                columns: new[] { "CurrentLeg", "NextPosibbleLegs" },
                values: new object[] { 32, 128 });

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CurrentLeg", "NextPosibbleLegs" },
                values: new object[] { 64, 128 });

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CurrentLeg", "NextPosibbleLegs" },
                values: new object[] { 128, 8 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 2,
                column: "NextPosibbleLegs",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CurrentLeg", "NextPosibbleLegs" },
                values: new object[] { 3, 4 });

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CurrentLeg", "NextPosibbleLegs" },
                values: new object[] { 4, 13 });

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CurrentLeg", "NextPosibbleLegs" },
                values: new object[] { 5, 7 });

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CurrentLeg", "NextPosibbleLegs" },
                values: new object[] { 6, 8 });

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CurrentLeg", "NextPosibbleLegs" },
                values: new object[] { 7, 8 });

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CurrentLeg", "NextPosibbleLegs" },
                values: new object[] { 8, 4 });
        }
    }
}
