using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightSimulator.Migrations
{
    /// <inheritdoc />
    public partial class basic4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CurrentLegId",
                table: "Flights",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeparture",
                table: "Flights",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PilotId",
                table: "Flights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Legs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentLeg = table.Column<int>(type: "int", nullable: false),
                    NextPosibbleLegs = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Legs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pilots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pilots", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_CurrentLegId",
                table: "Flights",
                column: "CurrentLegId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_PilotId",
                table: "Flights",
                column: "PilotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Legs_CurrentLegId",
                table: "Flights",
                column: "CurrentLegId",
                principalTable: "Legs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Pilots_PilotId",
                table: "Flights",
                column: "PilotId",
                principalTable: "Pilots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Legs_CurrentLegId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Pilots_PilotId",
                table: "Flights");

            migrationBuilder.DropTable(
                name: "Legs");

            migrationBuilder.DropTable(
                name: "Pilots");

            migrationBuilder.DropIndex(
                name: "IX_Flights_CurrentLegId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_PilotId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "CurrentLegId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "IsDeparture",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "PilotId",
                table: "Flights");
        }
    }
}
