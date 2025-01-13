using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OkumaVeIzlemeTakipSistemi.Context;
using OkumaVeIzlemeTakipSistemi.Models;

namespace OkumaVeIzlemeTakipSistemi.Controllers
{
    public class AnaSayfaViewModel
    {
        public int OkunacakKitapSayisi { get; set; }
        public int OkunanKitapSayisi { get; set; }
        public int IzlenecekDiziFilmSayisi { get; set; }
        public int IzlenenDiziFilmSayisi { get; set; }
    }

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly VeritabaniContext _context;

        public HomeController(ILogger<HomeController> logger, VeritabaniContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var kullaniciId = Request.Cookies["KullaniciId"];

            var model = new AnaSayfaViewModel
            {
                OkunacakKitapSayisi = _context.Icerikler.Count(k => k.Durum == "Okunacak" && k.KullaniciId == int.Parse(kullaniciId!)),
                OkunanKitapSayisi = _context.Icerikler.Count(k => k.Durum == "Okundu" && k.KullaniciId == int.Parse(kullaniciId!)),
                IzlenecekDiziFilmSayisi = _context.Icerikler.Count(d => d.Durum == "Ýzlenecek" && d.KullaniciId == int.Parse(kullaniciId!)),
                IzlenenDiziFilmSayisi = _context.Icerikler.Count(d => d.Durum == "Ýzlendi" && d.KullaniciId == int.Parse(kullaniciId!))
            };

            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
