using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Progi.BidCalculator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalculatorSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FeeType = table.Column<int>(type: "INTEGER", nullable: false),
                    VehicleType = table.Column<int>(type: "INTEGER", nullable: true),
                    ApplyMinMaxLimits = table.Column<bool>(type: "INTEGER", nullable: false),
                    Minimum = table.Column<decimal>(type: "TEXT", nullable: false),
                    Maximum = table.Column<decimal>(type: "TEXT", nullable: false),
                    RateType = table.Column<int>(type: "INTEGER", nullable: false),
                    Rate = table.Column<decimal>(type: "TEXT", nullable: false),
                    From = table.Column<decimal>(type: "TEXT", nullable: false),
                    To = table.Column<decimal>(type: "TEXT", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculatorSettings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CalculatorSettings",
                columns: new[] { "Id", "ApplyMinMaxLimits", "CreationDate", "FeeType", "From", "Maximum", "Minimum", "Rate", "RateType", "To", "UpdateDate", "VehicleType" },
                values: new object[,]
                {
                    { 1L, true, new DateTime(2024, 6, 25, 4, 32, 10, 81, DateTimeKind.Utc).AddTicks(499), 0, 0m, 50m, 10m, 10m, 0, null, null, 0 },
                    { 2L, true, new DateTime(2024, 6, 25, 4, 32, 10, 81, DateTimeKind.Utc).AddTicks(507), 0, 0m, 200m, 25m, 10m, 0, null, null, 1 },
                    { 3L, false, new DateTime(2024, 6, 25, 4, 32, 10, 81, DateTimeKind.Utc).AddTicks(509), 3, 0m, 0m, 0m, 2m, 0, null, null, 0 },
                    { 4L, false, new DateTime(2024, 6, 25, 4, 32, 10, 81, DateTimeKind.Utc).AddTicks(511), 3, 0m, 0m, 0m, 4m, 0, null, null, 1 },
                    { 5L, false, new DateTime(2024, 6, 25, 4, 32, 10, 81, DateTimeKind.Utc).AddTicks(517), 2, 1m, 0m, 0m, 5m, 1, 500m, null, null },
                    { 6L, false, new DateTime(2024, 6, 25, 4, 32, 10, 81, DateTimeKind.Utc).AddTicks(520), 2, 501m, 0m, 0m, 10m, 1, 1000m, null, null },
                    { 7L, false, new DateTime(2024, 6, 25, 4, 32, 10, 81, DateTimeKind.Utc).AddTicks(522), 2, 1001m, 0m, 0m, 15m, 1, 3000m, null, null },
                    { 8L, false, new DateTime(2024, 6, 25, 4, 32, 10, 81, DateTimeKind.Utc).AddTicks(525), 2, 3001m, 0m, 0m, 20m, 1, null, null, null },
                    { 9L, false, new DateTime(2024, 6, 25, 4, 32, 10, 81, DateTimeKind.Utc).AddTicks(566), 1, 0m, 0m, 0m, 100m, 1, null, null, 0 },
                    { 10L, false, new DateTime(2024, 6, 25, 4, 32, 10, 81, DateTimeKind.Utc).AddTicks(568), 1, 0m, 0m, 0m, 100m, 1, null, null, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculatorSettings");
        }
    }
}
