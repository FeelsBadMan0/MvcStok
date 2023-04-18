using MvcStok.Models.Entity;
using PagedList;
using System.Linq;
using System.Web.Mvc;

namespace MvcStok.Controllers
{
    public class MusterilerController : Controller
    {
        // GET: Musteriler
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index(int sayfa = 1)
        {
            //var musteriListe = db.tblmusteri.ToList();
            var musteriListe = db.tblmusteri.Where(x => x.durum == true).ToList().ToPagedList(sayfa, 3);
            return View(musteriListe);
        }

        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMusteri(tblmusteri m)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            else
            {
                m.durum = true;
                db.tblmusteri.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }

        public ActionResult MusteriSil(tblmusteri m)
        {
            var musteriBul = db.tblmusteri.Find(m.id);
            musteriBul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            var musteri = db.tblmusteri.Find(id);
            return View(musteri);
        }

        public ActionResult MusteriGuncelle(tblmusteri m)
        {
            var musteri = db.tblmusteri.Find(m.id);
            musteri.ad = m.ad;
            musteri.soyad = m.soyad;
            musteri.sehir = m.sehir;
            musteri.bakiye = m.bakiye;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}