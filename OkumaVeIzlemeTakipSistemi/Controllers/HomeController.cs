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
            var model = new AnaSayfaViewModel
            {
                OkunacakKitapSayisi = _context.Icerikler.Count(k => k.Durum == "Okunacak"),
                OkunanKitapSayisi = _context.Icerikler.Count(k => k.Durum == "Okundu"),
                IzlenecekDiziFilmSayisi = _context.Icerikler.Count(d => d.Durum == "Izlenecek"),
                IzlenenDiziFilmSayisi = _context.Icerikler.Count(d => d.Durum == "Izlendi")
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
