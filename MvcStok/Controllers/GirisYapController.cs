using MvcStok.Models.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcStok.Controllers
{
    public class GirisYapController : Controller
    {
        // GET: GirisYap
        DbMvcStokEntities db = new DbMvcStokEntities();
        [HttpGet]
        public ActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Giris(tbladmin a)
        {
            var bilgiler = db.tbladmin.FirstOrDefault(x => x.kullanici == a.kullanici && x.sifre == a.sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.kullanici, false);
                return RedirectToAction("Index", "Musteriler");
            }
            else
            {
                return View();
            }

        }

        public ActionResult Cikis()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Giris", "GirisYap");
        }
    }
}