using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceEntriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CarId = table.Column<int>(type: "integer", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    ServiceLeaflet = table.Column<int>(type: "integer", nullable: false),
                    InsertedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceEntries", x => x.Id);
                });

            // Add data to table
            migrationBuilder.InsertData(
                table: "ServiceEntries",
                columns: new[] { "CarId", "ServiceId", "ServiceLeaflet", "InsertedOn" },
                values: new object[] { 2, 1, 1, DateTime.UtcNow });
            migrationBuilder.InsertData(
                table: "ServiceEntries",
                columns: new[] { "CarId", "ServiceId", "ServiceLeaflet", "InsertedOn" },
                values: new object[] { 1, 3, 2, DateTime.UtcNow });
            migrationBuilder.InsertData(
                table: "ServiceEntries",
                columns: new[] { "CarId", "ServiceId", "ServiceLeaflet", "InsertedOn" },
                values: new object[] { 3, 3, 3, DateTime.UtcNow });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceEntries");
        }
    }
}
