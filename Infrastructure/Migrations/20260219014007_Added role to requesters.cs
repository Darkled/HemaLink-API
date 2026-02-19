using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addedroletorequesters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Account",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Role" },
                values: new object[] { "$2a$11$lGqHZpIqPENUqZsFTZmh9uNDs7XZoTSjmc6QASBHrrHuDcP0LACba", 2 });

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Password", "Role" },
                values: new object[] { "$2a$11$mkJkDilJmtOa6..QOdjrSeN.LOGxfaI97dbINaMkcggY9RbffzKm6", 1 });

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Password", "Role" },
                values: new object[] { "$2a$11$Nl43Whql4pHjRmDEB5PhiesmRrULEZ1hKNxz/Wyz5kx0oeLLJT/Lm", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Account",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Role" },
                values: new object[] { "$2a$11$N7G6W/AvtDo3xJkxWM664eRDmcYulvWvwb.09q5T381GvicCyny4K", 1 });

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Password", "Role" },
                values: new object[] { "$2a$11$wRKnnwjzr/zEwV3uUsSuyu3dQxU0Fkv98ejMi334ncv1r8eeTf6PS", 0 });

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$zfzhWyeQnBPvmZd3Kir3eeE9HoigS5eqioH5R4Ulmf1soMEGQe3Ym");
        }
    }
}
