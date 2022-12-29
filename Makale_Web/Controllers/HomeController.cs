using Makale_BusinessLayer;
using Makale_Common;
using Makale_Entities;
using Makale_Entities.ViewModel;
using Makale_Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Makale_Web.Controllers
{
    [Exc]
    public class HomeController : Controller
    {
        NotYonet ny = new NotYonet();
        KullaniciYonet ky = new KullaniciYonet();
        KategoriYonet kty = new KategoriYonet();
        // GET: Home
        public ActionResult Index()
        {

            //int s = 1;
            //int y = s / 0;

            //Test test1 = new Test();
            //test1.InsertTest();
            //  test1.UpdateTest();
            // test1.DeleteTest();
            //  test1.YorumEkle();
     
            //return View(ny.Listele().OrderByDescending(x => x.DegistirmeTarihi).ToList());
            return View(ny.ListeleQueryable().Where(x=>x.Taslak==false).OrderByDescending(x=>x.DegistirmeTarihi).ToList());
        }
        public ActionResult Kategori(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }           
           
            //Kategori kategori = kty.KategoriBul(id.Value);

            //if(kategori == null)
            //{
            //    return HttpNotFound();
            //}

            List<Not> notlar = ny.ListeleQueryable().Where(x => x.Taslak == false && x.KategoriId == id).OrderByDescending(x => x.DegistirmeTarihi).ToList();

            //return View("Index",kategori.Notlar.Where(x=>x.Taslak==false).OrderByDescending(x=>x.DegistirmeTarihi));
           
            return View("Index", notlar);
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

                Uygulama.kullaniciad = sonuc.nesne.KullaniciAdi;

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
        
            Uygulama.kullaniciad = model.KullaniciAdi;
           
            if (ModelState.IsValid)
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

        [Auth]
        public ActionResult ProfilGoster()
        {
            Kullanici kullanici =(Kullanici)Session["login"];


            return View(kullanici);
        }

        [Auth]
        public ActionResult ProfilDegistir()
        {
            Kullanici kullanici = (Kullanici)Session["login"];

            return View(kullanici);
        }

        [Auth]
        [HttpPost]
        public ActionResult ProfilDegistir(Kullanici kullanici, HttpPostedFileBase profilresmi)
        {
            Uygulama.kullaniciad = kullanici.KullaniciAdi;

            ModelState.Remove("DegistirenKullanici");

            if (ModelState.IsValid)
            {
              
                if (profilresmi != null && (profilresmi.ContentType == "image/jpeg" || profilresmi.ContentType == "image/jpg" || profilresmi.ContentType == "image/png"))
                {
                    string dosyaadi = $"user_{kullanici.Id}.{profilresmi.ContentType.Split('/')[1]}";
                    profilresmi.SaveAs(Server.MapPath($"~/image/{dosyaadi}"));
                    kullanici.ProfilResim = dosyaadi;
                }

                BusinessLayerSonuc<Kullanici> sonuc = ky.KullaniciUpdate(kullanici);

                if (sonuc.Hatalar.Count > 0)
                {
                    sonuc.Hatalar.ForEach(x => ModelState.AddModelError("", x));
                    return View(sonuc.nesne);
                }
                return RedirectToAction("ProfilGoster");
            }             

            return View(kullanici);
        }

        [Auth]
        public ActionResult ProfilSil()
        {
            Kullanici kullanici = Session["login"] as Kullanici;

            BusinessLayerSonuc<Kullanici> sonuc = ky.KullaniciSil(kullanici.Id);

            if(sonuc.Hatalar.Count > 0)
            {
                //Hatalar ekranda gösterilir. Profilsil ekranı oluşturabilirsiniz.
                sonuc.Hatalar.ForEach(x => ModelState.AddModelError("", x));
                return RedirectToAction("ProfilGoster",sonuc.nesne);
            }

            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult ErrorPage()
        {
            return View();
        }

    }
}