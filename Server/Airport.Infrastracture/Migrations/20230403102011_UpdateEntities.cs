using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airport.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Pilots",
                newName: "LastName");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Pilots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Pilots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Pilots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "Code",
                table: "Flights",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlainId",
                table: "Flights",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

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
                name: "IX_Flights_PlainId",
                table: "Flights",
                column: "PlainId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Plain_PlainId",
                table: "Flights");

            migrationBuilder.DropTable(
                name: "Plain");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Flights_PlainId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Pilots");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Pilots");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Pilots");

            migrationBuilder.DropColumn(
                name: "PlainId",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Pilots",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
