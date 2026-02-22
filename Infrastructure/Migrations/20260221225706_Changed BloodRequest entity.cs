using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedBloodRequestentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FulfilledOn",
                table: "BloodRequests");

            migrationBuilder.RenameColumn(
                name: "RequestedOn",
                table: "BloodRequests",
                newName: "RequestDate");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$SI/erk49fbe.0D3QvFv1pOiVajO8ZXOD1JyazCzkS8sn/N65nEC8O");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$FVOp/xJMMUc0WEZqPo/uAOJNEhBZNLQpmHaXDicrYKczvyDTQpT2O");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$dU/jEJfsw3uMJUzL9eOXVeSQ3LhCTZ9Hf10mLOAw5.KjHkK6f47si");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequestDate",
                table: "BloodRequests",
                newName: "RequestedOn");

            migrationBuilder.AddColumn<DateTime>(
                name: "FulfilledOn",
                table: "BloodRequests",
                type: "timestamp with time zone",
                nullable: true);

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
    }
}
