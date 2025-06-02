using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flower.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class InitDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 2, 15, 41, 21, 692, DateTimeKind.Unspecified).AddTicks(987), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 2, 15, 41, 21, 692, DateTimeKind.Unspecified).AddTicks(989), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 2, 15, 41, 21, 692, DateTimeKind.Unspecified).AddTicks(991), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 2, 15, 41, 21, 692, DateTimeKind.Unspecified).AddTicks(992), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 2, 15, 41, 21, 692, DateTimeKind.Unspecified).AddTicks(993), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 2, 15, 41, 21, 692, DateTimeKind.Unspecified).AddTicks(994), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "be24bc61-1150-436f-9863-558d4ce0089b", new DateTimeOffset(new DateTime(2025, 6, 2, 15, 41, 21, 692, DateTimeKind.Unspecified).AddTicks(1018), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 2, 15, 41, 21, 692, DateTimeKind.Unspecified).AddTicks(1019), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEI8burY+/5UQSTLWXRgIdd8p5ubZ57k6ChAoGW+8zXgQW+r5LceNkerx5zEetdYmXg==", "11e9d049-b63d-4c40-8a9f-4c33fc753279" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "529ff41d-b99c-4b7d-b0a0-cbd4327f0ef4", new DateTimeOffset(new DateTime(2025, 6, 2, 15, 41, 21, 745, DateTimeKind.Unspecified).AddTicks(4763), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 2, 15, 41, 21, 745, DateTimeKind.Unspecified).AddTicks(4809), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAENXWTT1K2abIH36Gdpo9ZJirPLE40ai/KyYAna2ua8pnFyFpbB98PvE4jLI8QQ5VrA==", "cad6ef36-2cd6-4a31-b2d2-4c1965639b71" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c6f6bd01-f303-40af-92cf-58dcb2c62af3", new DateTimeOffset(new DateTime(2025, 6, 2, 15, 41, 21, 800, DateTimeKind.Unspecified).AddTicks(5341), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 2, 15, 41, 21, 800, DateTimeKind.Unspecified).AddTicks(6357), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEJaskjTrj56Gze48BoNSZr2QMOUuEmm8Lr1jv3tOp0JkZgT9qpwFiYmBaYLtqa36RA==", "115dd03e-1311-47f8-bf16-7465d3b69904" });

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 2, 15, 41, 21, 692, DateTimeKind.Unspecified).AddTicks(533), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 2, 15, 41, 21, 692, DateTimeKind.Unspecified).AddTicks(554), new TimeSpan(0, 7, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 1, 20, 21, 38, 450, DateTimeKind.Unspecified).AddTicks(1929), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 1, 20, 21, 38, 450, DateTimeKind.Unspecified).AddTicks(1930), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 1, 20, 21, 38, 450, DateTimeKind.Unspecified).AddTicks(1932), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 1, 20, 21, 38, 450, DateTimeKind.Unspecified).AddTicks(1933), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 1, 20, 21, 38, 450, DateTimeKind.Unspecified).AddTicks(1934), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 1, 20, 21, 38, 450, DateTimeKind.Unspecified).AddTicks(1934), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b37b338-4f88-4f1a-9f89-d6516cba7d43", new DateTimeOffset(new DateTime(2025, 6, 1, 20, 21, 38, 450, DateTimeKind.Unspecified).AddTicks(2015), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 1, 20, 21, 38, 450, DateTimeKind.Unspecified).AddTicks(2016), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEHd7pMuPivIAT7ge6c2DZ248DTNpqCCD0dfljjkIB8vDp35OmCMSAtOiliKsXsxV+A==", "f2d1694e-3a72-4178-9a66-3d2aec4e3ffd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07c48ecc-855e-43d4-b166-34b4acf7a3c1", new DateTimeOffset(new DateTime(2025, 6, 1, 20, 21, 38, 510, DateTimeKind.Unspecified).AddTicks(3878), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 1, 20, 21, 38, 510, DateTimeKind.Unspecified).AddTicks(3928), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEFOdmtzLYbWk8ayKF8YN1WzoISOcibO3cw4GBjJej7z9xD3KZ8r58TB5lNig9rZCSw==", "39e22817-5899-46da-83a6-99bd1247fad5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedTime", "LastUpdatedTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e283fc82-8478-404e-a88f-b2da3d7959df", new DateTimeOffset(new DateTime(2025, 6, 1, 20, 21, 38, 574, DateTimeKind.Unspecified).AddTicks(2241), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 1, 20, 21, 38, 574, DateTimeKind.Unspecified).AddTicks(2291), new TimeSpan(0, 7, 0, 0, 0)), "AQAAAAIAAYagAAAAEODdvbyPTOmsFP+wYQGXEpcM3z8TU4fm3Tfay7tyEi4Y3GhIsP7ounrx6es+OOuGBQ==", "296afee3-4773-4b95-9aa5-8dcea5f2f994" });

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "LastUpdatedTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 1, 20, 21, 38, 450, DateTimeKind.Unspecified).AddTicks(1766), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 1, 20, 21, 38, 450, DateTimeKind.Unspecified).AddTicks(1774), new TimeSpan(0, 7, 0, 0, 0)) });
        }
    }
}
