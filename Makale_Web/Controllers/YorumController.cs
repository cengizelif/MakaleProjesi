using Makale_BusinessLayer;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Makale_Web.Controllers
{
    public class YorumController : Controller
    {
        // GET: Yorum
        public ActionResult YorumGoster(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            NotYonet ny = new NotYonet();
            Not not=ny.NotBul(id.Value);
           
            return PartialView("_PartialPageYorumlar", not.Yorumlar);
        }
    }
}