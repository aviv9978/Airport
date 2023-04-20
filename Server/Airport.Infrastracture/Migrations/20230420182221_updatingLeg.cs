using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airport.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class updatingLeg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FlightId",
                table: "Legs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOccupied",
                table: "Legs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FlightId", "IsOccupied" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FlightId", "IsOccupied" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FlightId", "IsOccupied" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "FlightId", "IsOccupied" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "FlightId", "IsOccupied" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "FlightId", "IsOccupied" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "FlightId", "IsOccupied" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "FlightId", "IsOccupied" },
                values: new object[] { null, false });

            migrationBuilder.CreateIndex(
                name: "IX_Legs_FlightId",
                table: "Legs",
                column: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Legs_Flights_FlightId",
                table: "Legs",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Legs_Flights_FlightId",
                table: "Legs");

            migrationBuilder.DropIndex(
                name: "IX_Legs_FlightId",
                table: "Legs");

            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "Legs");

            migrationBuilder.DropColumn(
                name: "IsOccupied",
                table: "Legs");
        }
    }
}
