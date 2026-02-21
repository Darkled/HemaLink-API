using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Removedbloodrequestlocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "BloodRequests");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "BloodRequests");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "BloodRequests");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$cGYy0UtwjJO.Cckgy4xEA.PeSW8tUAFEOZYSV2cM1K6XmgHPrdFbq");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$g/.03MJNsfrnKftxCQT1j.qLxf8fTiGyTvQ7bz4/XKgdCuNq9ZIIC");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$agpD1705I7KhJVDWmNQSiu59UOrvS2rB9bkReHk6PUbMpA.tWDBf.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "BloodRequests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "BloodRequests",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "BloodRequests",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$8SI9SHMLUK.rA3NbiY0BquE0tjdYSmzUppMVv2x2INUdUxkF41.3O");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$4j2Ds7Oy3TnGHd4RSfGatOJr4/nk05ag.THPT07B/bAyDUgGEYRni");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$R8sf7vX7CFFmkLXzZAJsDOSmyfxnCQ6IGDHQiwtyOzrmfiidOdvWW");
        }
    }
}
