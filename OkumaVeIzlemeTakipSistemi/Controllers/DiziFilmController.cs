using Microsoft.AspNetCore.Mvc;
using OkumaVeIzlemeTakipSistemi.Context;
using OkumaVeIzlemeTakipSistemi.Models;

namespace OkumaVeIzlemeTakipSistemi.Controllers
{
    public class DiziFilmController : Controller
    {
        private readonly VeritabaniContext _context;

        public DiziFilmController(VeritabaniContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var kullaniciId = Request.Cookies["KullaniciId"];
            var diziFilmler = _context.Icerikler
                .Where(i => i.Tur == "DiziFilm" && i.KullaniciId == int.Parse(kullaniciId!))
                .ToList();

            return View(diziFilmler);
        }

        [HttpPost]
        public IActionResult Ekle(IcerikModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var kullaniciId = Request.Cookies["KullaniciId"];
                    model.Tur = "DiziFilm";
                    model.KullaniciId = int.Parse(kullaniciId!);

                    _context.Icerikler.Add(model);
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata oluştu: {ex.Message}");
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DurumGuncelle(int id, string durum)
        {
            var diziFilm = _context.Icerikler.FirstOrDefault(i => i.Id == id);
            if (diziFilm != null)
            {
                diziFilm.Durum = durum;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
