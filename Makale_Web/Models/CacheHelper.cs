using Makale_BusinessLayer;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Makale_Web.Models
{
    public class CacheHelper
    {
        public static List<Kategori> Kategoriler()
        {
            var sonuc = WebCache.Get("kategori-cache");

            if(sonuc==null)
            {
                KategoriYonet ky = new KategoriYonet();
                sonuc = ky.Listele();
                WebCache.Set("kategori-cache",sonuc, 20, true);
            }

            return sonuc;
        }
    }
}