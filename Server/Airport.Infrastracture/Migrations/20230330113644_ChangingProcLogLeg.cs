using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airport.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class ChangingProcLogLeg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessLogger_Legs_LegId",
                table: "ProcessLogger");

            migrationBuilder.DropIndex(
                name: "IX_ProcessLogger_LegId",
                table: "ProcessLogger");

            migrationBuilder.DropColumn(
                name: "LegId",
                table: "ProcessLogger");

            migrationBuilder.AddColumn<int>(
                name: "Leg",
                table: "ProcessLogger",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Leg",
                table: "ProcessLogger");

            migrationBuilder.AddColumn<int>(
                name: "LegId",
                table: "ProcessLogger",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessLogger_LegId",
                table: "ProcessLogger",
                column: "LegId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessLogger_Legs_LegId",
                table: "ProcessLogger",
                column: "LegId",
                principalTable: "Legs",
                principalColumn: "Id");
        }
    }
}
