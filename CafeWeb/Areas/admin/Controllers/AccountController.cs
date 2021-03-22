using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CafeWeb.Models;
using System.Web.Security;

namespace CafeWeb.Areas.admin.Controllers
{
    
    public class AccountController : Controller
    {
        // GET: admin/Account
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Admin admin,string ReturnUrl)
        {

            if (!ModelState.IsValid)
            {
                return View("Login", admin);
            }
            using (CafeDbEntities db=new CafeDbEntities())
            {
                var res = db.Admin.FirstOrDefault(m => m.kullaniciAdi == admin.kullaniciAdi && m.Sifre == admin.Sifre);
                if (res!=null)
                {
                    FormsAuthentication.SetAuthCookie(res.kullaniciAdi,admin.BeniHatirla);
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    return RedirectToAction("Index", "Urunler");
                }
                ViewBag.Hata = "Kullancı adı veya şifre hatalı";
                return View();
            }
           
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }
    }
}