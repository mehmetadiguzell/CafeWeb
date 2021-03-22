using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CafeWeb.Models;

namespace CafeWebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        //[Authorize(Roles ="admin")]
        public ActionResult Index()
        {
            return View();
        }
        [Route("about")]
        public ActionResult About()
        {
            using(CafeDbEntities db=new CafeDbEntities())
            {
                var model = db.Hakkimizda.FirstOrDefault(m=>m.Id==1);
                return View(model);
            }

            
        }
        [Route("products")]
        public ActionResult Products()
        {
            using (CafeDbEntities db = new CafeDbEntities())
            {
                var model = db.Urunler.Where(m=>m.Aktiflik==true).OrderBy(m=>m.Sira).ToList();
                return View(model);
            }
        }

        [Route("products/{id}/{title}")]
        public ActionResult ProductDetails(int id)
        {
            using (CafeDbEntities db = new CafeDbEntities())
            {
                var model = db.Urunler.Where(m => m.Aktiflik == true && m.Id == id).FirstOrDefault();
                if (model==null)
                {
                    return HttpNotFound();
                }
                return View(model);
            }
        }


        [Route("store")]
        public ActionResult Store()
        {
            return View();
        }
        [Route("contact")]
        public ActionResult Contact()
        {
            return View();
        }
    }
}