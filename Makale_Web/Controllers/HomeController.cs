using Makale_BusinessLayer;
using Makale_Entities;
using Makale_Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Makale_Web.Controllers
{
    public class HomeController : Controller
    {
        NotYonet ny = new NotYonet();
        KullaniciYonet ky = new KullaniciYonet();
        KategoriYonet kty = new KategoriYonet();
        // GET: Home
        public ActionResult Index()
        {
            //Test test1 = new Test();
            //test1.InsertTest();
            //  test1.UpdateTest();
            // test1.DeleteTest();
            //  test1.YorumEkle();
     
            //return View(ny.Listele().OrderByDescending(x => x.DegistirmeTarihi).ToList());
            return View(ny.ListeleQueryable().OrderByDescending(x=>x.DegistirmeTarihi).ToList());
        }
        public ActionResult Kategori(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }           
           
            Kategori kategori = kty.KategoriBul(id.Value);

            if(kategori == null)
            {
                return HttpNotFound();
            }
            return View("Index",kategori.Notlar);
        }

        public ActionResult Begenilenler()
        {
            return View("Index",ny.Listele().OrderByDescending(x=>x.BegeniSayisi).ToList());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Login()
        {         
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
              BusinessLayerSonuc<Kullanici> sonuc=ky.LoginKontrol(model);
                if(sonuc.Hatalar.Count>0)
                {
                    sonuc.Hatalar.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }

                Session["login"] = sonuc.nesne; //Session da login olan kullanıcı bilgileri tutuldu.
                return RedirectToAction("Index");//Login olduğu için indexe yönlendirildi.
            }

            return View(model); 
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
        public ActionResult KayitOl()
        {
            return View();
        }            

        [HttpPost]
        public ActionResult KayitOl(KayitModel model)
        {
            if(ModelState.IsValid)
            {
               
               BusinessLayerSonuc<Kullanici> sonuc=ky.Kaydet(model);

                if(sonuc.Hatalar.Count>0)
                {
                   sonuc.Hatalar.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }

                return RedirectToAction("KayitBasarili");
            }

            return View(model);
        }

        public ActionResult KayitBasarili()
        {
            return View();
        }
        public ActionResult UserActivate(Guid id)
        {
            BusinessLayerSonuc<Kullanici> sonuc = ky.ActivateUser(id);

            if(sonuc.Hatalar.Count>0)
            {
                TempData["hatalar"] = sonuc.Hatalar;
                return RedirectToAction("UserActivateHata");
            }          

            return View();
        }
        public ActionResult UserActivateHata()
        {
            List<string> hatalar = null;

            if(TempData["hatalar"]!=null)
            {
                hatalar =(List<string>)TempData["hatalar"];
            }

            return View(hatalar);
        }

        public ActionResult ProfilGoster()
        {
            Kullanici kullanici =(Kullanici)Session["login"];


            return View(kullanici);
        }

        public ActionResult ProfilDegistir()
        {
            Kullanici kullanici = (Kullanici)Session["login"];

            return View(kullanici);
        }

        [HttpPost]
        public ActionResult ProfilDegistir(Kullanici kullanici,HttpPostedFile profilresmi)
        {
            if(profilresmi!=null && (profilresmi.ContentType=="image/jpeg" || profilresmi.ContentType=="image/jpg" || profilresmi.ContentType=="image/png"))
            {
                string dosyaadi = $"user_{kullanici.Id}.{profilresmi.ContentType.Split('/')[1]}";
                profilresmi.SaveAs(Server.MapPath($"~/image/{dosyaadi}"));
                kullanici.ProfilResim = dosyaadi;
            }

            BusinessLayerSonuc<Kullanici> sonuc=ky.KullaniciUpdate(kullanici);
            if(sonuc.Hatalar.Count>0)
            {
                sonuc.Hatalar.ForEach(x => ModelState.AddModelError("", x));
                return View(sonuc.nesne);
            }

            return View(kullanici);
        }

        public ActionResult ProfilSil()
        {
            return View();
        }

    }
}