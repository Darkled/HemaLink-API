using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updatedentitiesandnewdataseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CancellationToken",
                table: "Appointments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "Appointments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Name", "Password" },
                values: new object[] { "admin@example.com", "Admin", "$2a$11$fq5.Vt0fX7eNqZRQkpSymu.knpL90erYskefe.h5ml6Eed65XBP12" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Name", "Password" },
                values: new object[] { "mod@example.com", "Moderador", "$2a$11$EKinuHg6XFRxS9aU6vnNtepk8mqJb3YaxZs9S4PEP.McobDDbPvs6" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "Name", "Password" },
                values: new object[] { "heca@example.com", "Hospital Emergencias Clemente Álvarez", "$2a$11$bClCfIUOiNSwX1dpvOTOK.iPTP/kLv0ehTjZ6evDIY0FyLXkjStce" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountType", "AdmissionStatus", "Email", "IsActive", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { 4, "Requester", 1, "italiano@example.com", true, "Hospital Italiano", "$2a$11$JAUJ4IHItkKYeEhbMKLTeOZaRI1Mw3c5VM/SezHnvAysce6I5sAfm", "Requester" },
                    { 5, "Requester", 1, "parque@example.com", true, "Sanatorio Parque", "$2a$11$AyyhcGe518dZjgM0rnplKuj/AsOv/Adjpn2l.1ENL1wnu.ehh7zmu", "Requester" },
                    { 6, "Requester", 0, "centenario@example.com", true, "Hospital Centenario", "$2a$11$su/RbxD6q776KyBXkU9HbOINKE.GbdL5DkdkoXB.w.pITz3ffmznW", "Requester" },
                    { 7, "Requester", 1, "clinica@example.com", true, "Clínica de la Mujer", "$2a$11$x8BtOik9PZV0CCIoqOzzZu4n1PPGHh3POameGvekd.SZMLCvieVk6", "Requester" }
                });

            migrationBuilder.InsertData(
                table: "BloodRequests",
                columns: new[] { "Id", "Address", "BloodTypesNeeded", "RemainingUnits", "RequestDate", "RequestStatus", "RequesterId", "TargetUnits" },
                values: new object[,]
                {
                    { 1, "Pellegrini 3205, Rosario", new[] { 7, 6 }, 3, new DateTime(2026, 2, 26, 2, 30, 53, 616, DateTimeKind.Utc).AddTicks(5872), 0, 3, 5 },
                    { 2, "Pellegrini 3205, Rosario", new[] { 0 }, 0, new DateTime(2026, 2, 23, 2, 30, 53, 616, DateTimeKind.Utc).AddTicks(5891), 1, 3, 2 },
                    { 6, "Pellegrini 3205, Rosario", new[] { 7 }, 2, new DateTime(2026, 2, 24, 7, 30, 53, 616, DateTimeKind.Utc).AddTicks(5901), 0, 3, 2 },
                    { 9, "Pellegrini 3205, Rosario", new[] { 4 }, 8, new DateTime(2026, 2, 14, 2, 30, 53, 616, DateTimeKind.Utc).AddTicks(5907), 2, 3, 8 }
                });

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "juan.perez@example.com", "Juan Pérez", "3411234567" });

            migrationBuilder.InsertData(
                table: "Donors",
                columns: new[] { "Id", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 2, "maria.garcia@example.com", "María García", "3417654321" },
                    { 3, "carlos.lopez@example.com", "Carlos López", "1198765432" },
                    { 4, "ana.mtz@example.com", "Ana Martínez", "3415554443" },
                    { 5, "lucia.f@example.com", "Lucía Fernández", "3410009998" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "BloodRequestId", "CancellationToken", "DonorId", "IsCancelled" },
                values: new object[,]
                {
                    { 1, 1, "meVuNSBc1w5v88fPCg28+h5qzlysU8S2pFkQE7lY/RI=", 1, false },
                    { 2, 1, "1Srh18AcS+fqMjjLHT1+7X5H9fWLVxFJpifGz8HLhKI=", 2, false },
                    { 4, 2, "E6qU8tnkXSE3u5Sy+INDlZEYEQwDVIceHMF0O2fdDTY=", 4, false },
                    { 5, 2, "o7yR2/5M30r5Qc19VT2oPdEH65v6LoRPu9s3PJ05HHQ=", 5, false }
                });

            migrationBuilder.InsertData(
                table: "BloodRequests",
                columns: new[] { "Id", "Address", "BloodTypesNeeded", "RemainingUnits", "RequestDate", "RequestStatus", "RequesterId", "TargetUnits" },
                values: new object[,]
                {
                    { 3, "Virasoro 1249, Rosario", new[] { 3, 5 }, 10, new DateTime(2026, 3, 1, 2, 30, 53, 616, DateTimeKind.Utc).AddTicks(5893), 0, 4, 10 },
                    { 4, "Bv. Oroño 860, Rosario", new[] { 6 }, 1, new DateTime(2026, 2, 25, 2, 30, 53, 616, DateTimeKind.Utc).AddTicks(5897), 0, 5, 3 },
                    { 5, "San Luis 2450, Rosario", new[] { 1 }, 4, new DateTime(2026, 3, 6, 2, 30, 53, 616, DateTimeKind.Utc).AddTicks(5899), 0, 7, 4 },
                    { 7, "Virasoro 1249, Rosario", new[] { 2 }, 0, new DateTime(2026, 2, 19, 2, 30, 53, 616, DateTimeKind.Utc).AddTicks(5903), 1, 4, 1 },
                    { 8, "Bv. Oroño 860, Rosario", new[] { 6, 0 }, 6, new DateTime(2026, 2, 27, 2, 30, 53, 616, DateTimeKind.Utc).AddTicks(5905), 0, 5, 6 },
                    { 10, "San Luis 2450, Rosario", new[] { 7 }, 2, new DateTime(2026, 3, 11, 2, 30, 53, 616, DateTimeKind.Utc).AddTicks(5909), 0, 7, 2 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "BloodRequestId", "CancellationToken", "DonorId", "IsCancelled" },
                values: new object[,]
                {
                    { 3, 4, "ERF66vtnfDQNma/WWdEzoNAKfRyWHIqG8xHxrTUfKrU=", 3, false },
                    { 6, 3, "aU9sOGEOA/+KMHjfEIg7L8DbN3RKCdRin7rM3qIY2Qw=", 1, true },
                    { 7, 8, "vb//68t0hDlHgjJDDFNQcO8wqTNzDqRAtf4PU4FHhgM=", 2, false },
                    { 8, 10, "SgurdbJg/5s8fz8FSQnTmFr9FnGj0HMTevLko1AV1I8=", 4, false },
                    { 9, 7, "gWhxgQ5W9Ocy5lxwUiA/lQwvW4qxJ222SZq/I86Ci78=", 5, false },
                    { 10, 4, "HGSq9wAozxcR6czCNWKncUp/ygKII2w8TJDuIjF5d4o=", 2, true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "BloodRequests",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "CancellationToken",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "Appointments");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Name", "Password" },
                values: new object[] { "admin@email.com", "admin", "$2a$11$ZSBD60EgCRaoYOWsEWY5JuI8sg2brhz1V4sie11xzm.HphJa4cwEy" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Name", "Password" },
                values: new object[] { "mod@email.com", "mod", "$2a$11$DB4/cFqNO6rrQdybQeNjd.hYV6oCpRPMOir6OwMaka6zkxxSnEr7K" });

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
                columns: new[] { "Email", "Name", "Phone" },
                values: new object[] { "donor@email.com", "donor", "1234567890123" });
        }
    }
}
