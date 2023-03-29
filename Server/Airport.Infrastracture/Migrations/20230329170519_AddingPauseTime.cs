using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Airport.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class AddingPauseTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PauseTime",
                table: "Legs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Legs",
                columns: new[] { "Id", "CurrentLeg", "LegType", "NextPosibbleLegs", "PauseTime" },
                values: new object[,]
                {
                    { 1, 1, 2, 2, 3 },
                    { 2, 2, 4, 4, 4 },
                    { 3, 4, 4, 8, 5 },
                    { 4, 8, 4, 544, 4 },
                    { 5, 32, 4, 64, 3 },
                    { 6, 64, 1, 128, 4 },
                    { 7, 64, 1, 128, 4 },
                    { 8, 128, 4, 8, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "PauseTime",
                table: "Legs");
        }
    }
}
