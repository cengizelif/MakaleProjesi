using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Makale_BusinessLayer;
using Makale_Entities;

namespace Makale_Web.Controllers
{
    public class KullaniciController : Controller
    {
        KullaniciYonet ky = new KullaniciYonet();

        // GET: Kullanici
        public ActionResult Index()
        {
            return View(ky.Listele());
        }

        // GET: Kullanici/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanici kullanici = ky.KullanciBul(id.Value);
           
            if (kullanici == null)
            {
                return HttpNotFound();
            }
            return View(kullanici);
        }

        // GET: Kullanici/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kullanici kullanici)
        {
            ModelState.Remove("DegistirenKullanici");

            if (ModelState.IsValid)
            {
                BusinessLayerSonuc<Kullanici> sonuc = ky.KullaniciKaydet(kullanici); 
                if (sonuc.Hatalar.Count > 0)
                {
                    sonuc.Hatalar.ForEach(x => ModelState.AddModelError("", x));
                    return View(kullanici);
                }

                return RedirectToAction("Index");
            }          

            return View(kullanici);
        }

        // GET: Kullanici/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanici kullanici = ky.KullanciBul(id.Value);
            if (kullanici == null)
            {
                return HttpNotFound();
            }
            return View(kullanici);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                ky.KullaniciUpdate(kullanici);

                return RedirectToAction("Index");
            }
            return View(kullanici);
        }

        // GET: Kullanici/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kullanici kullanici = ky.KullanciBul(id.Value);
            if (kullanici == null)
            {
                return HttpNotFound();
            }
            return View(kullanici);
        }

        // POST: Kullanici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ky.KullaniciSil(id);

            return RedirectToAction("Index");
        }

        
    }
}
