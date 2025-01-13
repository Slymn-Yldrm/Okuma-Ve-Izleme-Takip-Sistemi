using Microsoft.AspNetCore.Mvc;
using OkumaVeIzlemeTakipSistemi.Context;
using OkumaVeIzlemeTakipSistemi.Models;

namespace OkumaVeIzlemeTakipSistemi.Controllers
{
    public class KitaplarController : Controller
    {
        private readonly VeritabaniContext _context;

        public KitaplarController(VeritabaniContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var kullaniciId = Request.Cookies["KullaniciId"];
            var kitaplar = _context.Icerikler
            .Where(i => i.Tur == "Kitap" && i.KullaniciId == int.Parse(kullaniciId!))
            .ToList();

            return View(kitaplar);
        }

        [HttpPost]
        public IActionResult Ekle(IcerikModel model)
        {
            Console.WriteLine("Ekle metodu tetiklendi.");
            if (ModelState.IsValid)
            {
                try
                {
                    Console.WriteLine($"Model geçerli: {model.Baslik} - {model.Durum}");
                    var kullaniciId = Request.Cookies["KullaniciId"]; model.Tur = "Kitap";
                    model.KullaniciId = int.Parse(kullaniciId!);

                    Console.WriteLine($"Eklenecek veri: {model.Baslik}, {model.Tur}, {model.Durum}, KullanıcıId: {model.KullaniciId}");

                    _context.Icerikler.Add(model);
                    _context.SaveChanges();

                    Console.WriteLine("Veritabanına başarıyla eklendi.");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata oluştu: {ex.Message}");
                }
            }
            else if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine($"Hata: {error.ErrorMessage}");
                    }
                }
            }
            else
            {
                Console.WriteLine("ModelState geçersiz.");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DurumGuncelle(int id, string durum)
        {
            var kitap = _context.Icerikler.FirstOrDefault(i => i.Id == id);
            if (kitap != null)
            {
                kitap.Durum = durum;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }



    }
}
