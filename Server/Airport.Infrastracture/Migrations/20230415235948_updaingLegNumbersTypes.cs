using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airport.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class updaingLegNumbersTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LegNum",
                table: "ProcessLogger");

            migrationBuilder.AddColumn<string>(
                name: "LegNumber",
                table: "ProcessLogger",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LegNumber",
                table: "ProcessLogger");

            migrationBuilder.AddColumn<int>(
                name: "LegNum",
                table: "ProcessLogger",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
