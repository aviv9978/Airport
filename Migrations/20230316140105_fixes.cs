using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightSimulator.Migrations
{
    /// <inheritdoc />
    public partial class fixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Fligts",
                table: "Fligts");

            migrationBuilder.RenameTable(
                name: "Fligts",
                newName: "Flights");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flights",
                table: "Flights",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Flights",
                table: "Flights");

            migrationBuilder.RenameTable(
                name: "Flights",
                newName: "Fligts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fligts",
                table: "Fligts",
                column: "Id");
        }
    }
}
