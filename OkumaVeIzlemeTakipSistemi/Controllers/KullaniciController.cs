using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using OkumaVeIzlemeTakipSistemi.Context;
using OkumaVeIzlemeTakipSistemi.Models;

namespace OkumaVeIzlemeTakipSistemi.Controllers
{
    public class KullaniciController : Controller
    {
        private readonly VeritabaniContext _context;
        private readonly PasswordHasher<KullaniciModel> _passwordHasher;

        public KullaniciController(VeritabaniContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<KullaniciModel>();
        }

        [HttpGet]
        public IActionResult KayitOl()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GirisYap()
        {
            return View();
        }

        [HttpPost]
        public IActionResult KayitOl(KullaniciModel model)
        {
            if (ModelState.IsValid)
            {
                model.Sifre = _passwordHasher.HashPassword(model, model.Sifre);

                _context.Kullanicilar.Add(model);
                _context.SaveChanges();

                return RedirectToAction("GirisYap");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult GirisYap(string kullaniciAdi, string sifre)
        {
            var kullanici = _context.Kullanicilar.SingleOrDefault(k => k.KullaniciAdi == kullaniciAdi);

            if (kullanici != null)
            {
                var result = _passwordHasher.VerifyHashedPassword(kullanici, kullanici.Sifre, sifre);
                if (result == PasswordVerificationResult.Success)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Hata = "Kullanıcı adı veya şifre hatalı.";
            return View();
        }
    }
}
