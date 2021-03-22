using CafeWeb.Controllers;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CafeWeb.Models;
using System.IO;

namespace CafeWeb.Areas.admin.Controllers
{
    [Authorize]
    public class HakkimizdaController : Controller
    {
        // GET: admin/Hakkimizda
        public ActionResult Index()
        {
            using (CafeDbEntities db=new CafeDbEntities())
            {
                var res = db.Hakkimizda.FirstOrDefault(m=>m.Id==1);
                return View(res);
            }
            
        }
        public ActionResult HakkimizdaGuncelle()
        {
            using (CafeDbEntities db = new CafeDbEntities())
            {
                var model = db.Hakkimizda.FirstOrDefault(m => m.Id == 1);
                return View(model);
            }

        }

        public ActionResult Kaydet(Hakkimizda updateAbout)
        {
            using (CafeDbEntities db = new CafeDbEntities())
            {
                var model = db.Hakkimizda.FirstOrDefault(m => m.Id == 1);
                if (!ModelState.IsValid)
                {
                    return View("HakkimizdaGuncelle", updateAbout);
                }
                if (updateAbout.fotoFile!=null)
                {
                    updateAbout.aboutImage = Seo.DosyaAdiDuzenle(updateAbout.fotoFile.FileName);
                    updateAbout.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), Path.GetFileName(updateAbout.aboutImage)));
                }
                model.UstBaslik = updateAbout.UstBaslik;
                model.Baslik = updateAbout.Baslik;
                model.Aciklama = updateAbout.Aciklama;
                model.aboutImage = updateAbout.aboutImage;

                db.SaveChanges();
                TempData["bilgilendirme"] = " ";
                return RedirectToAction("Index");
            }

        }
    }
}