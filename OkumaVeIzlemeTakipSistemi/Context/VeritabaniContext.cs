namespace OkumaVeIzlemeTakipSistemi.Context
{
    using Microsoft.EntityFrameworkCore;
    using OkumaVeIzlemeTakipSistemi.Models;

    public class VeritabaniContext : DbContext
    {
        public VeritabaniContext(DbContextOptions<VeritabaniContext> options) : base(options)
        {
        }

        public DbSet<KullaniciModel> Kullanicilar { get; set; }
        public DbSet<IcerikModel> Icerikler { get; set; }
    }
}
