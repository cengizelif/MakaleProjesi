using Makale_DataAccessLayer;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BusinessLayer
{
    public class Test
    {        
        public Test()
        {
            //DatabaseContext db = new DatabaseContext();
            ////db.Database.CreateIfNotExists();
            //db.Kategoriler.ToList();

            Repository<Kategori> rep_kat = new Repository<Kategori>();
            List<Kategori> kategoriler=rep_kat.Liste();
            List<Kategori> kategoriler2 = rep_kat.Liste(x => x.Id > 5);
        }
        Repository<Kullanici> rep_kul = new Repository<Kullanici>();
        public void InsertTest()
        {
           
            rep_kul.Insert(new Kullanici() { 
             Ad="deneme",Soyad="deneme",Email="email",
             Aktif=true,AktifGuid=Guid.NewGuid(),
             Admin=true,KullaniciAdi="deneme",Sifre="123", 
             KayitTarih=DateTime.Now,
             DegistirmeTarihi=DateTime.Now.AddDays(1),
             DegistirenKullanici="deneme"
            });
        }

        public void UpdateTest()
        {
            Kullanici kullanici = rep_kul.Find(x => x.KullaniciAdi == "deneme");
            if(kullanici != null)
            {
                kullanici.KullaniciAdi = "test";
                rep_kul.Update(kullanici);
            }
        }

        public void DeleteTest()
        {
            Kullanici kullanici = rep_kul.Find(x => x.KullaniciAdi == "test");
            if(kullanici!=null)
            {
                rep_kul.Delete(kullanici);
            }
        }

        public void YorumEkle()
        {
            Repository<Yorum> rep_yorum = new Repository<Yorum>();
            Repository<Not> rep_not = new Repository<Not>();

            Kullanici kullanici = rep_kul.Find(x => x.Id == 1);
            Not not = rep_not.Find(x => x.Id == 1);

            rep_yorum.Insert(new Yorum() {
              Kullanici=kullanici,
              Not=not,
              Text="deneme yorum",
              KayitTarih=DateTime.Now,
              DegistirmeTarihi=DateTime.Now.AddHours(1),
              DegistirenKullanici=kullanici.KullaniciAdi         
            
            });
        }

    }
}
