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
                Email = "admin@email.com",
                Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                Role = Role.Admin
            };

            Staff mod = new Staff()
            {
                Id = 2,
                Name = "mod",
                Email = "mod@email.com",
                Password = BCrypt.Net.BCrypt.HashPassword("mod"),
                Role = Role.Moderator
            };

            Requester requester = new Requester()
            {
                Id = 3,
                Name = "requester",
                Email = "requester@email.com",
                Password = BCrypt.Net.BCrypt.HashPassword("requester"),
                Role = Role.Requester,
                AdmissionStatus = AdmissionStatus.Accepted
            };

            Donor donor = new Donor()
            {
                Id = 1,
                Name = "donor",
                Email = "donor@email.com",
                Phone = "1234567890123"
            };

            modelBuilder.Entity<Staff>().HasData(admin, mod);
            modelBuilder.Entity<Donor>().HasData(donor);
            modelBuilder.Entity<Requester>().HasData(requester);

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