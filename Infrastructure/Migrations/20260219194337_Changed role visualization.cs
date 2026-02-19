using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Changedrolevisualization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Account",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Role" },
                values: new object[] { "$2a$11$/amHvGj682EmwVnYtNGeDOBPjZBJuPzwC08whqqkAnuz.MqxGLDs6", "Admin" });

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Password", "Role" },
                values: new object[] { "$2a$11$4ALuPFSevKgWZLvzqd7acuts5YTDcn/jdA2WPZL4JsGvE3R6JESAm", "Moderator" });

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Password", "Role" },
                values: new object[] { "$2a$11$/tyTaVsZsDl0EFmLVg7uqu5rLqS3dZpz6KQ45MVzL5J3tSVbY30ZW", "User" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Account",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

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
    }
}
