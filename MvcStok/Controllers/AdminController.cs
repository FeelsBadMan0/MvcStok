using MvcStok.Models.Entity;
using System.Web.Mvc;

namespace MvcStok.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult YeniAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniAdmin(tbladmin a)
        {
            db.tbladmin.Add(a);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}