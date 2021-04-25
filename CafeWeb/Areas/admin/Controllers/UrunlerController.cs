using CafeWeb.Controllers;
using CafeWeb.Models;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeWeb.Areas.admin.Controllers
{
    [Authorize]
    public class UrunlerController : Controller
    {
        // GET: admin/Urunler
        
        public ActionResult Index()
        {
            using (CafeDbEntities db = new CafeDbEntities())
            {
                var res = db.Urunler.ToList();
                return View(res);
            }
        }
        public ActionResult Yeni()
        {
            var model = new Urunler();
            return View("urunFormu",model);
        }

        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            using (CafeDbEntities db = new CafeDbEntities())
            {
                var res = db.Urunler.Find(id);
                if (res==null)
                {
                    return HttpNotFound();
                }
                return View("urunFormu", res);
            }
        }

        [HttpPost]
        public ActionResult Kaydet(Urunler gelenUrun)
        {
            using (var db = new CafeDbEntities())
            {
                if (gelenUrun.Id==0) //ekleme
                {
                    string fotoAdi = Seo.DosyaAdiDuzenle(gelenUrun.urunFile.FileName);
                    gelenUrun.urunImage = fotoAdi;
                    Urunler urunler = new Urunler()
                    {
                        UstBaslik=gelenUrun.UstBaslik,
                        Baslik=gelenUrun.Baslik,
                        urunImage=gelenUrun.urunImage,
                        Aciklama=gelenUrun.Aciklama,
                        Aktiflik=gelenUrun.Aktiflik,
                        Sira=gelenUrun.Sira
                    };        
                    db.Urunler.Add(urunler);
                    gelenUrun.urunFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), Path.GetFileName(fotoAdi)));
                    TempData["Urun"] = "Ürün Eklendi";
                 }
                else //güncelleme
                {
                    var guncellenecekVeri = db.Urunler.Find(gelenUrun.Id);

                    if (gelenUrun.urunFile != null)
                    {
                        string fotoAdi = Seo.DosyaAdiDuzenle(gelenUrun.urunFile.FileName);
                        gelenUrun.urunImage = fotoAdi;
                        gelenUrun.urunFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), Path.GetFileName(fotoAdi)));
                    }
                    
                    db.Entry(guncellenecekVeri).CurrentValues.SetValues(gelenUrun);
                    
                    
                    TempData["Urun"] = " ";
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Sil(int id=0)
        {
            using (CafeDbEntities db = new CafeDbEntities())
            {
                var model = db.Urunler.Find(id);
                if (model==null)
                {
                    return HttpNotFound();
                }
                db.Urunler.Remove(model);
                db.SaveChanges();
                TempData["Urun"] = "Ürün silme işlemi başarılı";
                return RedirectToAction("Index");
            }

        }
    }
}