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
using Makale_Web.Filters;
using Makale_Web.Models;

namespace Makale_Web.Controllers
{
   
    public class NotController : Controller
    {
        NotYonet ny = new NotYonet();

        [Auth]
        public ActionResult Index()
        {
            var nots = ny.ListeleQueryable().Include(n => n.Kategori);

            if (Session["login"]!=null)
            {
                Kullanici kullanici = (Kullanici)Session["login"];
                nots = ny.ListeleQueryable().Include(n => n.Kategori).Where(x => x.Kullanici.Id == kullanici.Id);
            }
           
            return View(nots.ToList());           
        }

        LikeYonet ly = new LikeYonet();

        [Auth]
        public ActionResult Begendiklerim()
        {         
            var nots = ny.ListeleQueryable().Include(n => n.Kategori);
            if (Session["login"] != null)
            {
                Kullanici kullanici = (Kullanici)Session["login"];

                nots = ly.ListeleQueryable().Include("Kullanici").Include("Not").Where(x => x.Kullanici.Id == kullanici.Id).Select(x=>x.Not).Include(k=>k.Kategori);
            }
            return View("Index",nots.ToList());
        }

        [Auth]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Not not = ny.NotBul(id.Value);

            if (not == null)
            {
                return HttpNotFound();
            }
            return View(not);
        }

        KategoriYonet ky = new KategoriYonet();

        [Auth]
        public ActionResult Create()
        {            
            ViewBag.KategoriId = new SelectList(CacheHelper.Kategoriler(), "Id", "Baslik");
            return View();
        }

        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Not not)
        {
            Kullanici kullanici = null;

            if (Session["login"] != null)
            {
                kullanici = (Kullanici)Session["login"];
            }

            not.Kullanici = kullanici;

            ViewBag.KategoriId = new SelectList(CacheHelper.Kategoriler(), "Id", "Baslik", not.KategoriId);

            ModelState.Remove("DegistirenKullanici");

            if (ModelState.IsValid)
            {
                BusinessLayerSonuc<Not> sonuc=ny.NotKaydet(not);
                if (sonuc.Hatalar.Count > 0)
                {
                    sonuc.Hatalar.ForEach(x => ModelState.AddModelError("", x));                
                    return View(not);
                }

                return RedirectToAction("Index");
            }
           
            return View(not);
        }

        [Auth]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Not not = ny.NotBul(id.Value);
            if (not == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(CacheHelper.Kategoriler(), "Id", "Baslik", not.KategoriId);
            return View(not);
        }

        [HttpPost]
        [Auth]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Not not)
        {
            ViewBag.KategoriId = new SelectList(CacheHelper.Kategoriler(), "Id", "Baslik", not.KategoriId);

            ModelState.Remove("DegistirenKullanici");

            if (ModelState.IsValid)
            {
               BusinessLayerSonuc<Not> sonuc=ny.NotUpdate(not);
                if (sonuc.Hatalar.Count > 0)
                {
                    sonuc.Hatalar.ForEach(x => ModelState.AddModelError("", x));
                    return View(not);
                }
                return RedirectToAction("Index");
            }
          
            return View(not);
        }

        [Auth]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Not not = ny.NotBul(id.Value);
            if (not == null)
            {
                return HttpNotFound();
            }
            return View(not);
        }

        [Auth]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Not not = ny.NotBul(id);
            
            BusinessLayerSonuc<Not> sonuc=ny.NotSil(not);
            if (sonuc.Hatalar.Count > 0)
            {
                sonuc.Hatalar.ForEach(x => ModelState.AddModelError("", x));
                return View(not);
            }

            return RedirectToAction("Index");
        }

        [Auth]
        [HttpPost]
        public ActionResult GetLikes(int[] id_dizi)
        {
            List<int> likenot = new List<int>();

            Kullanici kullanici = (Kullanici)Session["login"];

            if (kullanici!=null)
              likenot = ly.Listele(x => x.Kullanici.Id == kullanici.Id && id_dizi.Contains(x.Not.Id)).Select(x=>x.Not.Id).ToList();

            //select not_id from begeni where kullanici_id=2 and not_id in (1,5,8,9,6)

            return Json(new { sonuc = likenot });
        }

        public ActionResult NotDetay(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Not not = ny.NotBul(id.Value);

            return PartialView("_PartialPageNotDetay", not);
        }

     
        public ActionResult SetLike(int notid,bool like)
        {
            int sonuc = 0;

            Kullanici kullanici = (Kullanici)Session["login"];

            if (kullanici==null)
                return Json(new { hata = true, res = -1 });

              Not not = ny.NotBul(notid);
                Begeni begen = ly.BegeniBul(notid, kullanici.Id);

                if (begen != null && like == false)
                {
                    sonuc = ly.BegeniSil(begen);
                }
                else if (begen == null && like == true)
                {
                    sonuc = ly.BegeniEkle(new Begeni()
                    {
                        Kullanici = kullanici,
                        Not = not
                    });
                }

                if (sonuc > 0)
                {
                    if (like)
                    {
                        not.BegeniSayisi++;
                    }
                    else
                    {
                        not.BegeniSayisi--;
                    }

                    BusinessLayerSonuc<Not> notupdate = ny.NotUpdate(not);

                    if (notupdate.Hatalar.Count == 0)
                    {
                        return Json(new { hata = false, res = not.BegeniSayisi });
                    }
                }

               return Json(new { hata = true, res = not.BegeniSayisi });          


        }

    }
}
