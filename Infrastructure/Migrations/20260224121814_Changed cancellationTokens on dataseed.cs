using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedcancellationTokensondataseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$1MHAL7w8Nfx102.U4e6s4.9lWB96GN2fYDZa3uP6PXwLRy1lPiMmi");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$Lg6qYokWCmymc1zt69INnuOykpwo7dFEqd0Grh/HkH6E9hLVVvRI2");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$wSFgXhiaTSRF57Ggw/zd.OI3pVQdpxhmpsclMzIwNAbs6SW7XV0K.");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$OdfMdf96yzPXE4vjXQnt7.5tYIaZFYZYnahrPX0st9RGl7aHYMAAu");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "$2a$11$3ZMjSzFhhGRuiULzkRn.HOokv8RvUOK0eQ5Sw7NINwoJodCtTijqm");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6,
                column: "Password",
                value: "$2a$11$gHdgdHBM9ERDkQAM7.6nPe9zzAhRKbGAHS.bAq4/HB.yis1QNIA9K");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 7,
                column: "Password",
                value: "$2a$11$mHIgA8.OpHquLFtkReeEGer7kZQm7CHJfXpzbLc4WmzOb6iW6x4ZC");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CancellationToken",
                value: "cQE7UfT4h4k_mhain5VsHqWF5ifL2L3Zv6ZALCli5LY");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CancellationToken",
                value: "cshs1Emd-D4Avux2tX2zOILZekO0NMHRS9EIJ1ptFCI");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CancellationToken",
                value: "XFZB14bTQrZFp4zD6TokzORK3VVqap4GrI9uWVGP6YI");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CancellationToken",
                value: "3-I5hOuZcqtA75zP5Jh6JX8V3C2EQCMxzxoUXVQC038");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 5,
                column: "CancellationToken",
                value: "Hlhv3JdcKZu9qc--1rk1y_gZOTjDFoP1ZadNQVkxUu0");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 6,
                column: "CancellationToken",
                value: "3e2NUbkKlyfSUP1RITdhbE--qYtf-PWnf6JqnxIs360");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 7,
                column: "CancellationToken",
                value: "gakImI2b8f-LSS2qa_sG_AY_2B5aAwc-qBcGRbI_TzM");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 8,
                column: "CancellationToken",
                value: "ZO6wS39Rlipx31y3nwCNxqhcivFTU6tn8OyHfFN20bU");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 9,
                column: "CancellationToken",
                value: "jzXEPTZXtk1qjcGAqA1U5QG1TadefhrdUI_L_7ZuzOQ");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 10,
                column: "CancellationToken",
                value: "mT_3jnFlWZubQ_udT2Wpd1cKSQeCzyKf8Jx11m3CJlM");

            migrationBuilder.UpdateData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequestDate",
                value: new DateTime(2026, 2, 26, 12, 18, 13, 626, DateTimeKind.Utc).AddTicks(7946));

            migrationBuilder.UpdateData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 2,
                column: "RequestDate",
                value: new DateTime(2026, 2, 23, 12, 18, 13, 626, DateTimeKind.Utc).AddTicks(7961));

            migrationBuilder.UpdateData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 3,
                column: "RequestDate",
                value: new DateTime(2026, 3, 1, 12, 18, 13, 626, DateTimeKind.Utc).AddTicks(7963));

            migrationBuilder.UpdateData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 4,
                column: "RequestDate",
                value: new DateTime(2026, 2, 25, 12, 18, 13, 626, DateTimeKind.Utc).AddTicks(7966));

            migrationBuilder.UpdateData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 5,
                column: "RequestDate",
                value: new DateTime(2026, 3, 6, 12, 18, 13, 626, DateTimeKind.Utc).AddTicks(7968));

            migrationBuilder.UpdateData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 6,
                column: "RequestDate",
                value: new DateTime(2026, 2, 24, 17, 18, 13, 626, DateTimeKind.Utc).AddTicks(7970));

            migrationBuilder.UpdateData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 7,
                column: "RequestDate",
                value: new DateTime(2026, 2, 19, 12, 18, 13, 626, DateTimeKind.Utc).AddTicks(7973));

            migrationBuilder.UpdateData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 8,
                column: "RequestDate",
                value: new DateTime(2026, 2, 27, 12, 18, 13, 626, DateTimeKind.Utc).AddTicks(7975));

            migrationBuilder.UpdateData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 9,
                column: "RequestDate",
                value: new DateTime(2026, 2, 14, 12, 18, 13, 626, DateTimeKind.Utc).AddTicks(7977));

            migrationBuilder.UpdateData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 10,
                column: "RequestDate",
                value: new DateTime(2026, 3, 11, 12, 18, 13, 626, DateTimeKind.Utc).AddTicks(7979));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$fq5.Vt0fX7eNqZRQkpSymu.knpL90erYskefe.h5ml6Eed65XBP12");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$EKinuHg6XFRxS9aU6vnNtepk8mqJb3YaxZs9S4PEP.McobDDbPvs6");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$bClCfIUOiNSwX1dpvOTOK.iPTP/kLv0ehTjZ6evDIY0FyLXkjStce");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$JAUJ4IHItkKYeEhbMKLTeOZaRI1Mw3c5VM/SezHnvAysce6I5sAfm");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "$2a$11$AyyhcGe518dZjgM0rnplKuj/AsOv/Adjpn2l.1ENL1wnu.ehh7zmu");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6,
                column: "Password",
                value: "$2a$11$su/RbxD6q776KyBXkU9HbOINKE.GbdL5DkdkoXB.w.pITz3ffmznW");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 7,
                column: "Password",
                value: "$2a$11$x8BtOik9PZV0CCIoqOzzZu4n1PPGHh3POameGvekd.SZMLCvieVk6");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CancellationToken",
                value: "meVuNSBc1w5v88fPCg28+h5qzlysU8S2pFkQE7lY/RI=");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CancellationToken",
                value: "1Srh18AcS+fqMjjLHT1+7X5H9fWLVxFJpifGz8HLhKI=");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CancellationToken",
                value: "ERF66vtnfDQNma/WWdEzoNAKfRyWHIqG8xHxrTUfKrU=");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CancellationToken",
                value: "E6qU8tnkXSE3u5Sy+INDlZEYEQwDVIceHMF0O2fdDTY=");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 5,
                column: "CancellationToken",
                value: "o7yR2/5M30r5Qc19VT2oPdEH65v6LoRPu9s3PJ05HHQ=");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 6,
                column: "CancellationToken",
                value: "aU9sOGEOA/+KMHjfEIg7L8DbN3RKCdRin7rM3qIY2Qw=");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 7,
                column: "CancellationToken",
                value: "vb//68t0hDlHgjJDDFNQcO8wqTNzDqRAtf4PU4FHhgM=");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 8,
                column: "CancellationToken",
                value: "SgurdbJg/5s8fz8FSQnTmFr9FnGj0HMTevLko1AV1I8=");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 9,
                column: "CancellationToken",
                value: "gWhxgQ5W9Ocy5lxwUiA/lQwvW4qxJ222SZq/I86Ci78=");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 10,
                column: "CancellationToken",
                value: "HGSq9wAozxcR6czCNWKncUp/ygKII2w8TJDuIjF5d4o=");

            migrationBuilder.UpdateData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequestDate",
                value: new DateTime(2026, 2, 26, 2, 30, 53, 616, DateTimeKind.Utc).AddTicks(5872));

            migrationBuilder.UpdateData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 2,
                column: "RequestDate",
                value: new DateTime(2026, 2, 23, 2, 30, 53, 616, DateTimeKind.Utc).AddTicks(5891));

            migrationBuilder.UpdateData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 3,
                column: "RequestDate",
                value: new DateTime(2026, 3, 1, 2, 30, 53, 616, DateTimeKind.Utc).AddTicks(5893));

            migrationBuilder.UpdateData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 4,
                column: "RequestDate",
                value: new DateTime(2026, 2, 25, 2, 30, 53, 616, DateTimeKind.Utc).AddTicks(5897));

            migrationBuilder.UpdateData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 5,
                column: "RequestDate",
                value: new DateTime(2026, 3, 6, 2, 30, 53, 616, DateTimeKind.Utc).AddTicks(5899));

            migrationBuilder.UpdateData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 6,
                column: "RequestDate",
                value: new DateTime(2026, 2, 24, 7, 30, 53, 616, DateTimeKind.Utc).AddTicks(5901));

            migrationBuilder.UpdateData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 7,
                column: "RequestDate",
                value: new DateTime(2026, 2, 19, 2, 30, 53, 616, DateTimeKind.Utc).AddTicks(5903));

            migrationBuilder.UpdateData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 8,
                column: "RequestDate",
                value: new DateTime(2026, 2, 27, 2, 30, 53, 616, DateTimeKind.Utc).AddTicks(5905));

            migrationBuilder.UpdateData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 9,
                column: "RequestDate",
                value: new DateTime(2026, 2, 14, 2, 30, 53, 616, DateTimeKind.Utc).AddTicks(5907));

            migrationBuilder.UpdateData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 10,
                column: "RequestDate",
                value: new DateTime(2026, 3, 11, 2, 30, 53, 616, DateTimeKind.Utc).AddTicks(5909));
        }
    }
}
