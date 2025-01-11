namespace OkumaVeIzlemeTakipSistemi.Context
{
    using Microsoft.EntityFrameworkCore;
    using OkumaVeIzlemeTakipSistemi.Models;

    public class VeritabaniContext : DbContext
    {
        public DbSet<KullaniciModel> Kullanicilar { get; set; }
        public DbSet<IcerikModel> Icerikler { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=OkumaIzlemeTakipDB;Trusted_Connection=True;");
        }
    }

}
