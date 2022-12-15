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

            KategoriYonet ky = new KategoriYonet();
            Kategori kategori = ky.KategoriBul(id.Value);

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
            return View(model);
        }

        public ActionResult KayitOl()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KayitOl(KayitModel model)
        {
            return View(model);
        }

    }
}