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
                Role = Role.User,
                AdmissionStatus = AdmissionStatus.Accepted
            };

            Donator Gabriel = new Donator()
            {
                Id = 1,
                Name = "Gabriel",
                Email = "gabriel@mail.com",
                Phone = "1234567890123"
            };

            modelBuilder.Entity<Staff>().HasData(admin, mod);
            modelBuilder.Entity<Donator>().HasData(Gabriel);
            modelBuilder.Entity<Requester>().HasData(GruppeSechs);

            modelBuilder.Entity<Account>()
                .HasDiscriminator<string>("AccountType")
                .HasValue<Staff>("Staff")
                .HasValue<Requester>("Requester");

            modelBuilder.Entity<Account>()
                    .Property(a => a.Role)
                    .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }
    }
}