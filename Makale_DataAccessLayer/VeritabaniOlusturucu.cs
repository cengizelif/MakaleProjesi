using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_DataAccessLayer
{
    public class VeritabaniOlusturucu:CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            
            Kullanici admin = new Kullanici() 
            {
                 Ad="Elif",
                 Soyad="Cengiz",
                 Email="cenelif@gmail.com",
                 Sifre="1234",
                 Aktif=true,
                 Admin=true,
                 AktifGuid=Guid.NewGuid(),
                 KullaniciAdi="elif",
                 KayitTarih=DateTime.Now,
                 DegistirmeTarihi=DateTime.Now.AddMinutes(5),
                 DegistirenKullanici="elif"
            };

            context.Kullanicilar.Add(admin);

            for (int i = 1; i < 6; i++)
            {
                Kullanici user = new Kullanici()
                {
                     Ad=FakeData.NameData.GetFirstName(),
                     Soyad=FakeData.NameData.GetSurname(),
                     Email=FakeData.NetworkData.GetEmail(),
                     Sifre="1234",
                     KullaniciAdi="user"+i,
                     Aktif=true,
                     Admin=false,
                     AktifGuid=Guid.NewGuid(),
                      KayitTarih=FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1),DateTime.Now),
                      DegistirmeTarihi=DateTime.Now,
                      DegistirenKullanici= "user" +i
                };
                context.Kullanicilar.Add(user);
            }

            context.SaveChanges();

            List<Kullanici> kullanicilistesi = context.Kullanicilar.ToList();

            //Fake katogori ekleme
            for (int i = 0; i < 10; i++)
            {
                Kategori kat = new Kategori()
                {
                    Baslik=FakeData.PlaceData.GetStreetName(),
                    Aciklama=FakeData.PlaceData.GetAddress(),
                    KayitTarih=DateTime.Now,
                    DegistirmeTarihi=DateTime.Now,
                    DegistirenKullanici = "elif"
                };
                context.Kategoriler.Add(kat);

                //Fake not ekleme
                for (int j = 1; j < 6; j++)
                {
                    Not not = new Not()
                    {
                        Baslik=FakeData.TextData.GetAlphabetical(20),
                        Text=FakeData.TextData.GetSentences(3),
                        Taslak=false,
                        BegeniSayisi=FakeData.NumberData.GetNumber(1,9),
                        Kullanici= kullanicilistesi[FakeData.NumberData.GetNumber(0,5)],
                        KayitTarih = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        DegistirmeTarihi = DateTime.Now,
                        DegistirenKullanici= kullanicilistesi[FakeData.NumberData.GetNumber(0, 5)].KullaniciAdi,
                    };

                    kat.Notlar.Add(not);

                    //Fake yorum ekle

                    for (int k = 1; k < 4; k++)
                    {
                        Yorum y = new Yorum()
                        {
                            Text=FakeData.TextData.GetSentences(3),
                            Kullanici = kullanicilistesi[FakeData.NumberData.GetNumber(0, 5)],
                            KayitTarih = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            DegistirmeTarihi = DateTime.Now,
                            DegistirenKullanici = kullanicilistesi[FakeData.NumberData.GetNumber(0, 5)].KullaniciAdi
                        };

                        not.Yorumlar.Add(y);
                    }//yorum for

                    for (int x = 0; x < not.BegeniSayisi; x++)
                    {
                        Begeni b = new Begeni()
                        {
                            Kullanici = kullanicilistesi[FakeData.NumberData.GetNumber(0, 5)]
                        };

                        not.Begeniler.Add(b);
                    }//like for

                }//makale notu for

            }//kategori for
            context.SaveChanges();
            
        }

    }
}
