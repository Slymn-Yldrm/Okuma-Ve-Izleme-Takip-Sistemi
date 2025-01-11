namespace OkumaVeIzlemeTakipSistemi.Models
{
    public class IcerikModel
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Tur { get; set; }
        public string Durum { get; set; }
        public int KullaniciId { get; set; }
    }
}
