using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Changedseedstohaveregularemails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Password" },
                values: new object[] { "admin@email.com", "$2a$11$ZSBD60EgCRaoYOWsEWY5JuI8sg2brhz1V4sie11xzm.HphJa4cwEy" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Password" },
                values: new object[] { "mod@email.com", "$2a$11$DB4/cFqNO6rrQdybQeNjd.hYV6oCpRPMOir6OwMaka6zkxxSnEr7K" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "Name", "Password" },
                values: new object[] { "requester@email.com", "requester", "$2a$11$XLhiSZQYcsq3mp4PYMaeT.F5vWVyC79LD83RNun9kZVYr06tpfOWG" });

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Name" },
                values: new object[] { "donor@email.com", "donor" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Password" },
                values: new object[] { "admin", "$2a$11$0AN2D9itwF2iKuDleIBj4e1i47isByucMPBmshyIxz/ksfHDOzhb2" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Password" },
                values: new object[] { "mod", "$2a$11$u1RBYJlBcGFlDhd47jTgNuwGU8/lSFtzTvTrqS3u2msBv.oZbCLNS" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "Name", "Password" },
                values: new object[] { "gruppesechs@mail.com", "Gruppe Sechs", "$2a$11$ZrMDTCzm3KyDIHw6pJtPWeN3/29y1HQAdHVzuQSu0bTr1WvQPxGb2" });

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Name" },
                values: new object[] { "gabriel@mail.com", "Gabriel" });
        }
    }
}
