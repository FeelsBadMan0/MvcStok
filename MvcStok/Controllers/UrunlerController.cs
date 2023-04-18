using MvcStok.Models.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace MvcStok.Controllers
{
    public class UrunlerController : Controller
    {
        // GET: Urunler
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index(string p)
        {
            //var urunler = db.tblurunler.Where(x => x.durum == true).ToList();
            var urunler = db.tblurunler.Where(x => x.durum == true);
            if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(x => x.ad.Contains(p) && x.durum == true);
            }
            return View(urunler.ToList());
        }

        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> ktg = (from x in db.tblkategori.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad,
                                            Value = x.id.ToString()
                                        }).ToList();
            ViewBag.deger = ktg;
            return View();
        }


        [HttpPost]
        public ActionResult YeniUrun(tblurunler u)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> ktg = (from x in db.tblkategori.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.ad,
                                                Value = x.id.ToString()
                                            }).ToList();
                ViewBag.deger = ktg;
                return View("YeniUrun");
            }
            else
            {
                var ktgr = db.tblkategori.Where(x => x.id == u.tblkategori.id).FirstOrDefault();
                u.tblkategori = ktgr;
                db.tblurunler.Add(u);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }

        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> deger = (from x in db.tblkategori.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.ad,
                                              Value = x.id.ToString()
                                          }).ToList();
            ViewBag.dgr = deger;
            var urun = db.tblurunler.Find(id);
            return View("UrunGetir", urun);
        }

        public ActionResult UrunGuncelle(tblurunler u)
        {
            var urun = db.tblurunler.Find(u.id);
            urun.marka = u.marka;
            urun.ad = u.ad;
            urun.stok = u.stok;
            urun.alisfiyat = u.alisfiyat;
            urun.satisfiyat = u.satisfiyat;
            var ktg = db.tblkategori.Where(x => x.id == u.tblkategori.id).FirstOrDefault();
            urun.kategori = ktg.id;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunSil(tblurunler u)
        {
            var urunBul = db.tblurunler.Find(u.id);
            urunBul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}