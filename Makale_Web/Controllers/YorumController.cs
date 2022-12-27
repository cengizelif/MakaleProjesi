using Makale_BusinessLayer;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Makale_Web.Controllers
{
    public class YorumController : Controller
    {

        YorumYonet yy = new YorumYonet();
        NotYonet ny = new NotYonet();
        public ActionResult YorumGoster(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }      
            Not not=ny.NotBul(id.Value);
           
            return PartialView("_PartialPageYorumlar", not.Yorumlar);
        }

        [HttpPost]
        public ActionResult Edit(int? id,string text)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

          
            Yorum yorum=yy.YorumBul(id.Value);
           
            if(yorum==null)
            {
                return new HttpNotFoundResult();
            }
            yorum.Text = text;
          
            if(yy.YorumGuncelle(yorum)>0)
            {
                return Json(new {sonuc=true}, JsonRequestBehavior.AllowGet);    
            }

            return Json(new {sonuc=false}, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yorum yorum = yy.YorumBul(id.Value);

            if (yorum == null)
            {
                return new HttpNotFoundResult();
            }

            if (yy.YorumSil(yorum) > 0)
            {
                return Json(new { sonuc = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { sonuc = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(Yorum yorum,int? notid)
        {
            ModelState.Remove("DegistirenKullanici");

            if(ModelState.IsValid)
            {
                if(notid==null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Not not = ny.NotBul(notid.Value);

                if(not==null)
                {
                    return new HttpNotFoundResult();
                }

                yorum.Not = not;
                yorum.Kullanici =(Kullanici)Session["login"];
                int sonuc=yy.YorumEkle(yorum);

                if (sonuc > 0)
                {
                    return Json(new { sonuc = true }, JsonRequestBehavior.AllowGet);
                }
                
            }

            return Json(new { sonuc = false }, JsonRequestBehavior.AllowGet);
        }
    }
}