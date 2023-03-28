using FinalniTest.Models.Login;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalniTest.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Kurir> Kuriri { get; set; }
        public DbSet<Paket> Paketi { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kurir>().HasData(
                new Kurir() { Id = 1, Ime = "Marko Petrov", Rodjenje = 1987 },
                new Kurir() { Id = 2, Ime = "Andrea Marin", Rodjenje = 1990 },
                new Kurir() { Id = 3, Ime = "Viktor Pavlov", Rodjenje = 1987 }
            );

            modelBuilder.Entity<Paket>().HasData(
                new Paket()
                {
                    Id = 1,
                    Posiljalac = "Slike doo",
                    Primalac = "Galerija doo",
                    Tezina = 1.1m,
                    Postarina = 520,
                    KurirId = 3
                },
                new Paket()
                {
                    Id = 2,
                    Posiljalac = "Galerija doo",
                    Primalac = "Salon Centar",
                    Tezina = 0.9m,
                    Postarina = 340,
                    KurirId = 1
                },
                new Paket()
                {
                    Id = 3,
                    Posiljalac = "Best terarijumi",
                    Primalac = "Ljubimci sur",
                    Tezina = 5.4m,
                    Postarina = 2200,
                    KurirId = 2
                },
                new Paket()
                {
                    Id = 4,
                    Posiljalac = "Kul klime",
                    Primalac = "Izgradnja doo",
                    Tezina = 7.8m,
                    Postarina = 4500,
                    KurirId = 3
                },
                new Paket()
                {
                    Id = 5,
                    Posiljalac = "Galanterija szr",
                    Primalac = "Prav kroj szr",
                    Tezina = 2.2m,
                    Postarina = 800,
                    KurirId = 1
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
