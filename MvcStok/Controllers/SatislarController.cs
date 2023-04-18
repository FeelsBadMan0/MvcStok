using MvcStok.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcStok.Controllers
{
    public class SatislarController : Controller
    {
        // GET: Satislar
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index()
        {
            var satislar = db.tblsatislar.ToList();
            return View(satislar);
        }

        [HttpGet]
        public ActionResult YeniSatis()
        {

            //Ürünler
            List<SelectListItem> urun = (from x in db.tblurunler.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.ad,
                                             Value = x.id.ToString()
                                         }).ToList();
            ViewBag.deger1 = urun;

            //Müşteriler
            List<SelectListItem> mst = (from x in db.tblmusteri.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad + " " + x.soyad,
                                            Value = x.id.ToString()
                                        }).ToList();
            ViewBag.deger2 = mst;

            //Personel
            List<SelectListItem> prs = (from x in db.tblpersonel.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad + " " + x.soyad,
                                            Value = x.id.ToString()
                                        }).ToList();
            ViewBag.deger3 = prs;

            return View();
        }

        [HttpPost]
        public ActionResult YeniSatis(tblsatislar s)
        {

            var urun = db.tblurunler.Where(x => x.id == s.tblurunler.id).FirstOrDefault();
            var mst = db.tblmusteri.Where(x => x.id == s.tblmusteri.id).FirstOrDefault();
            var prs = db.tblpersonel.Where(x => x.id == s.tblpersonel.id).FirstOrDefault();
            s.tblurunler = urun;
            s.tblmusteri = mst;
            s.tblpersonel = prs;
            s.tarih = DateTime.Parse(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
            db.tblsatislar.Add(s);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}