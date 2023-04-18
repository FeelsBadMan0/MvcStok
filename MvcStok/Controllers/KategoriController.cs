using MvcStok.Models.Entity;
using System.Linq;
using System.Web.Mvc;
namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index()
        {
            var kategoriler = db.tblkategori.Where(x => x.durum == true).ToList();
            return View(kategoriler);
        }

        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniKategori(tblkategori p)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            else
            {
                db.tblkategori.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }

        public ActionResult KategoriSil(tblkategori k)
        {
            var ktgrBul = db.tblkategori.Find(k.id);
            ktgrBul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.tblkategori.Find(id);
            return View("KategoriGetir", ktgr);
        }

        public ActionResult KategoriGuncelle(tblkategori k)
        {
            var ktgr = db.tblkategori.Find(k.id);
            ktgr.ad = k.ad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}