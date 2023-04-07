using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airport.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Plain_PlainId",
                table: "Flights");

            migrationBuilder.DropTable(
                name: "Plain");

            migrationBuilder.RenameColumn(
                name: "LegId",
                table: "ProcessLogger",
                newName: "LegNum");

            migrationBuilder.CreateTable(
                name: "Plane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassangerCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plane", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plane_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plane_CompanyId",
                table: "Plane",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Plane_PlainId",
                table: "Flights",
                column: "PlainId",
                principalTable: "Plane",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Plane_PlainId",
                table: "Flights");

            migrationBuilder.DropTable(
                name: "Plane");

            migrationBuilder.RenameColumn(
                name: "LegNum",
                table: "ProcessLogger",
                newName: "LegId");

            migrationBuilder.CreateTable(
                name: "Plain",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassangerCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plain_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plain_CompanyId",
                table: "Plain",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Plain_PlainId",
                table: "Flights",
                column: "PlainId",
                principalTable: "Plain",
                principalColumn: "Id");
        }
    }
}
