using Makale_DataAccessLayer;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BusinessLayer
{
    public class KategoriYonet
    {
        Repository<Kategori> rep_kat = new Repository<Kategori>();
        public List<Kategori> Listele()
        {
            return rep_kat.Liste();
        }

        public Kategori KategoriBul(int id)
        {
            return rep_kat.Find(x => x.Id == id);
        }
        BusinessLayerSonuc<Kategori> sonuc = new BusinessLayerSonuc<Kategori>();
        public BusinessLayerSonuc<Kategori> KategoriEkle(Kategori kategori)
        {         
            sonuc.nesne=rep_kat.Find(x=>x.Baslik==kategori.Baslik);
            if(sonuc.nesne!=null)
            {
                sonuc.Hatalar.Add("Bu kategori kayıtlı.");
            }
            else
            {
               int kayit=rep_kat.Insert(kategori);
                if(kayit<1)
                {
                    sonuc.Hatalar.Add("Kategori kaydedilemedi.");
                }
            }

            return sonuc;
        }

        public BusinessLayerSonuc<Kategori> KategoriUpdate(Kategori kategori)
        {
            sonuc.nesne = rep_kat.Find(x => x.Id == kategori.Id);
            if(sonuc.nesne!=null)
            {
                sonuc.nesne.Baslik = kategori.Baslik;
                sonuc.nesne.Aciklama = kategori.Aciklama;
                int updatesonuc=rep_kat.Update(sonuc.nesne);
               
                if(updatesonuc<1)
                {
                    sonuc.Hatalar.Add("Kategori bilgileri değiştirilemedi.");
                }
            }

            return sonuc;
        }

        public BusinessLayerSonuc<Kategori> KategoriSil(Kategori kategori)
        {
            BusinessLayerSonuc<Kategori> sonuc = new BusinessLayerSonuc<Kategori>();
            sonuc.nesne = rep_kat.Find(x => x.Id == kategori.Id);

            Repository<Not> rep_not = new Repository<Not>();
            Repository<Yorum> rep_yorum = new Repository<Yorum>();
            Repository<Begeni> rep_begeni = new Repository<Begeni>();

            if (sonuc.nesne != null)
            {
                //foreach (Not not in sonuc.nesne.Notlar.ToList())
                //{
                //    foreach (Yorum yorum in not.Yorumlar.ToList())
                //    {
                //        rep_yorum.Delete(yorum);
                //        //yorumlar silinecek
                //    }

                //    foreach (Begeni begen in not.Begeniler.ToList())
                //    {
                //        rep_begeni.Delete(begen);
                //        //beğeniler silinecek
                //    }

                //    //notlar silinecek
                //    rep_not.Delete(not);
                //}

                int silsonuc = rep_kat.Delete(sonuc.nesne);//kategori siliniyor
                if (silsonuc < 1)
                    sonuc.Hatalar.Add("Kategori silinemedi.");
            }
            else
            {
                sonuc.Hatalar.Add("Kategori bulunamadı");
            }
            return sonuc;
        }
    }
}
