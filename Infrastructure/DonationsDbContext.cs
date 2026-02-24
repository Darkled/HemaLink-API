using Domain.Models;
using Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DonationsDbContext : DbContext
    {
        public DonationsDbContext(DbContextOptions<DonationsDbContext> options) : base(options)
        {

        }
        public DbSet<Account> Accounts { get; set;}
        public DbSet<Staff> Staff { get; set;}
        public DbSet<Requester> Requesters { get; set;}
        public DbSet<BloodRequest> BloodRequests { get; set;}
		public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Donor> Donors { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Staff>().HasData(
                new Staff
                {
                    Id = 1,
                    Name = "Admin",
                    Email = "admin@example.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("string"),
                    Role = Role.Admin
                },
                new Staff
                {
                    Id = 2,
                    Name = "Moderador",
                    Email = "mod@example.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("string"),
                    Role = Role.Moderator
                }
            );

            modelBuilder.Entity<Requester>().HasData(
                new Requester
                {
                    Id = 3,
                    Name = "Hospital Emergencias Clemente Álvarez",
                    Email = "heca@example.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("string"),
                    Role = Role.Requester,
                    AdmissionStatus = AdmissionStatus.Accepted
                },
                new Requester
                {
                    Id = 4,
                    Name = "Hospital Italiano",
                    Email = "italiano@example.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("string"),
                    Role = Role.Requester,
                    AdmissionStatus = AdmissionStatus.Accepted
                },
                new Requester
                {
                    Id = 5,
                    Name = "Sanatorio Parque",
                    Email = "parque@example.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("string"),
                    Role = Role.Requester,
                    AdmissionStatus = AdmissionStatus.Accepted
                },
                new Requester
                {
                    Id = 6,
                    Name = "Hospital Centenario",
                    Email = "centenario@example.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("string"),
                    Role = Role.Requester,
                    AdmissionStatus = AdmissionStatus.Pending
                },
                new Requester
                {
                    Id = 7,
                    Name = "Clínica de la Mujer",
                    Email = "clinica@example.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("string"),
                    Role = Role.Requester,
                    AdmissionStatus = AdmissionStatus.Accepted
                }
            );

            modelBuilder.Entity<Donor>().HasData(
                new Donor
                {
                    Id = 1,
                    Name = "Juan Pérez",
                    Email = "juan.perez@example.com",
                    Phone = "3411234567"
                },
                new Donor
                {
                    Id = 2,
                    Name = "María García",
                    Email = "maria.garcia@example.com",
                    Phone = "3417654321"
                },
                new Donor
                {
                    Id = 3,
                    Name = "Carlos López",
                    Email = "carlos.lopez@example.com",
                    Phone = "1198765432"
                },
                new Donor
                {
                    Id = 4,
                    Name = "Ana Martínez",
                    Email = "ana.mtz@example.com",
                    Phone = "3415554443"
                },
                new Donor
                {
                    Id = 5,
                    Name = "Lucía Fernández",
                    Email = "lucia.f@example.com",
                    Phone = "3410009998"
                }
            );

            modelBuilder.Entity<BloodRequest>().HasData(
                new BloodRequest
                {
                    Id = 1,
                    RequesterId = 3,
                    Address = "Pellegrini 3205, Rosario",
                    TargetUnits = 5,
                    RemainingUnits = 3,
                    RequestDate = DateTime.UtcNow.AddDays(2),
                    RequestStatus = RequestStatus.Open,
                    BloodTypesNeeded = new List<BloodType> { BloodType.O_Neg, BloodType.O_Pos }
                },
                new BloodRequest
                {
                    Id = 2,
                    RequesterId = 3,
                    Address = "Pellegrini 3205, Rosario",
                    TargetUnits = 2,
                    RemainingUnits = 0,
                    RequestDate = DateTime.UtcNow.AddDays(-1),
                    RequestStatus = RequestStatus.Completed,
                    BloodTypesNeeded = new List<BloodType> { BloodType.A_Pos }
                },
                new BloodRequest
                {
                    Id = 3,
                    RequesterId = 4,
                    Address = "Virasoro 1249, Rosario",
                    TargetUnits = 10,
                    RemainingUnits = 10,
                    RequestDate = DateTime.UtcNow.AddDays(5),
                    RequestStatus = RequestStatus.Open,
                    BloodTypesNeeded = new List<BloodType> { BloodType.B_Neg, BloodType.AB_Neg }
                },
                new BloodRequest
                {
                    Id = 4,
                    RequesterId = 5,
                    Address = "Bv. Oroño 860, Rosario",
                    TargetUnits = 3,
                    RemainingUnits = 1,
                    RequestDate = DateTime.UtcNow.AddDays(1),
                    RequestStatus = RequestStatus.Open,
                    BloodTypesNeeded = new List<BloodType> { BloodType.O_Pos }
                },
                new BloodRequest
                {
                    Id = 5,
                    RequesterId = 7,
                    Address = "San Luis 2450, Rosario",
                    TargetUnits = 4,
                    RemainingUnits = 4,
                    RequestDate = DateTime.UtcNow.AddDays(10),
                    RequestStatus = RequestStatus.Open,
                    BloodTypesNeeded = new List<BloodType> { BloodType.A_Neg }
                },
                new BloodRequest
                {
                    Id = 6,
                    RequesterId = 3,
                    Address = "Pellegrini 3205, Rosario",
                    TargetUnits = 2,
                    RemainingUnits = 2,
                    RequestDate = DateTime.UtcNow.AddHours(5),
                    RequestStatus = RequestStatus.Open,
                    BloodTypesNeeded = new List<BloodType> { BloodType.O_Neg }
                },
                new BloodRequest
                {
                    Id = 7,
                    RequesterId = 4,
                    Address = "Virasoro 1249, Rosario",
                    TargetUnits = 1,
                    RemainingUnits = 0,
                    RequestDate = DateTime.UtcNow.AddDays(-5),
                    RequestStatus = RequestStatus.Completed,
                    BloodTypesNeeded = new List<BloodType> { BloodType.B_Pos }
                },
                new BloodRequest
                {
                    Id = 8,
                    RequesterId = 5,
                    Address = "Bv. Oroño 860, Rosario",
                    TargetUnits = 6,
                    RemainingUnits = 6,
                    RequestDate = DateTime.UtcNow.AddDays(3),
                    RequestStatus = RequestStatus.Open,
                    BloodTypesNeeded = new List<BloodType> { BloodType.O_Pos, BloodType.A_Pos }
                },
                new BloodRequest
                {
                    Id = 9,
                    RequesterId = 3,
                    Address = "Pellegrini 3205, Rosario",
                    TargetUnits = 8,
                    RemainingUnits = 8,
                    RequestDate = DateTime.UtcNow.AddDays(-10),
                    RequestStatus = RequestStatus.Expired,
                    BloodTypesNeeded = new List<BloodType> { BloodType.AB_Pos }
                },
                new BloodRequest
                {
                    Id = 10,
                    RequesterId = 7,
                    Address = "San Luis 2450, Rosario",
                    TargetUnits = 2,
                    RemainingUnits = 2,
                    RequestDate = DateTime.UtcNow.AddDays(15),
                    RequestStatus = RequestStatus.Open,
                    BloodTypesNeeded = new List<BloodType> { BloodType.O_Neg }
                }
            );

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    Id = 1,
                    BloodRequestId = 1,
                    DonorId = 1,
                    IsCancelled = false,
                    CancellationToken = "cQE7UfT4h4k_mhain5VsHqWF5ifL2L3Zv6ZALCli5LY"
                },
                new Appointment
                {
                    Id = 2,
                    BloodRequestId = 1,
                    DonorId = 2,
                    IsCancelled = false,
                    CancellationToken = "cshs1Emd-D4Avux2tX2zOILZekO0NMHRS9EIJ1ptFCI"
                },
                new Appointment
                {
                    Id = 3,
                    BloodRequestId = 4,
                    DonorId = 3,
                    IsCancelled = false,
                    CancellationToken = "XFZB14bTQrZFp4zD6TokzORK3VVqap4GrI9uWVGP6YI"
                },
                new Appointment
                {
                    Id = 4,
                    BloodRequestId = 2,
                    DonorId = 4,
                    IsCancelled = false,
                    CancellationToken = "3-I5hOuZcqtA75zP5Jh6JX8V3C2EQCMxzxoUXVQC038"
                },
                new Appointment
                {
                    Id = 5,
                    BloodRequestId = 2,
                    DonorId = 5,
                    IsCancelled = false,
                    CancellationToken = "Hlhv3JdcKZu9qc--1rk1y_gZOTjDFoP1ZadNQVkxUu0"
                },
                new Appointment
                {
                    Id = 6,
                    BloodRequestId = 3,
                    DonorId = 1,
                    IsCancelled = true,
                    CancellationToken = "3e2NUbkKlyfSUP1RITdhbE--qYtf-PWnf6JqnxIs360"
                },
                new Appointment
                {
                    Id = 7,
                    BloodRequestId = 8,
                    DonorId = 2,
                    IsCancelled = false,
                    CancellationToken = "gakImI2b8f-LSS2qa_sG_AY_2B5aAwc-qBcGRbI_TzM"
                },
                new Appointment
                {
                    Id = 8,
                    BloodRequestId = 10,
                    DonorId = 4,
                    IsCancelled = false,
                    CancellationToken = "ZO6wS39Rlipx31y3nwCNxqhcivFTU6tn8OyHfFN20bU"
                },
                new Appointment
                {
                    Id = 9,
                    BloodRequestId = 7,
                    DonorId = 5,
                    IsCancelled = false,
                    CancellationToken = "jzXEPTZXtk1qjcGAqA1U5QG1TadefhrdUI_L_7ZuzOQ"
                },
                new Appointment
                {
                    Id = 10,
                    BloodRequestId = 4,
                    DonorId = 2,
                    IsCancelled = true,
                    CancellationToken = "mT_3jnFlWZubQ_udT2Wpd1cKSQeCzyKf8Jx11m3CJlM"
                }
);

            modelBuilder.Entity<Account>()
                .HasDiscriminator<string>("AccountType")
                .HasValue<Staff>("Staff")
                .HasValue<Requester>("Requester");

            modelBuilder.Entity<Account>()
                .Property(a => a.Role)
                .HasConversion<string>();

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Donor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DonorId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.BloodRequest)
                .WithMany(br => br.Appointments)
                .HasForeignKey(a => a.BloodRequestId);

			base.OnModelCreating(modelBuilder);
        }
    }
}