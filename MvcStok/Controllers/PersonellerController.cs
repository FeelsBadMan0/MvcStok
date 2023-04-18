using MvcStok.Models.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MvcStok.Controllers
{
    public class PersonellerController : Controller
    {
        // GET: Personeller
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index()
        {
            var personeller = db.tblpersonel.Where(x => x.durum == true).ToList();
            return View(personeller);
        }

        [HttpGet]
        public ActionResult YeniPersonel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniPersonel(tblpersonel p)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniPersonel");
            }
            else
            {
                db.tblpersonel.Add(p);
                p.durum = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult PersonelSil(tblpersonel p)
        {
            var personelBul = db.tblpersonel.Find(p.id);
            personelBul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelGetir(int id)
        {
            var pers = db.tblpersonel.Find(id);
            return View(pers);
        }

        public ActionResult PersonelGuncelle(tblpersonel p)
        {
            var pers = db.tblpersonel.Find(p.id);
            pers.ad = p.ad;
            pers.soyad = p.soyad;
            pers.departman = p.departman;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}