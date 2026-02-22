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
            Staff admin = new Staff ()
            {
                Id = 1,
                Name = "admin",
                Email = "admin",
                Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                Role = Role.Admin
            };

            Staff mod = new Staff()
            {
                Id = 2,
                Name = "mod",
                Email = "mod",
                Password = BCrypt.Net.BCrypt.HashPassword("mod"),
                Role = Role.Moderator
            };

            Requester GruppeSechs = new Requester()
            {
                Id = 3,
                Name = "Gruppe Sechs",
                Email = "gruppesechs@mail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("gruppesechs"),
                Role = Role.Requester,
                AdmissionStatus = AdmissionStatus.Accepted
            };

            Donor Gabriel = new Donor()
            {
                Id = 1,
                Name = "Gabriel",
                Email = "gabriel@mail.com",
                Phone = "1234567890123"
            };

            modelBuilder.Entity<Staff>().HasData(admin, mod);
            modelBuilder.Entity<Donor>().HasData(Gabriel);
            modelBuilder.Entity<Requester>().HasData(GruppeSechs);

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