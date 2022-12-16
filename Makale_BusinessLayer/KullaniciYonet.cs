using Makale_Common;
using Makale_DataAccessLayer;
using Makale_Entities;
using Makale_Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BusinessLayer
{
    public class KullaniciYonet
    {
        Repository<Kullanici> rep_kul = new Repository<Kullanici>();
        public BusinessLayerSonuc<Kullanici> Kaydet(KayitModel model)
        {
            BusinessLayerSonuc<Kullanici> sonuc= new BusinessLayerSonuc<Kullanici>();

            Kullanici kullanici = rep_kul.Find(x => x.KullaniciAdi == model.KullaniciAdi || x.Email == model.Email);

            if(kullanici!=null)
            {
                if(kullanici.KullaniciAdi==model.KullaniciAdi)
                {
                    sonuc.Hatalar.Add("Kullanıcı adı sistemde kayıtlı");
                }
                if(kullanici.Email==model.Email)
                {
                    sonuc.Hatalar.Add("Email sistemde kayıtlı");
                }
            }
            else
            {
               int kaydet= rep_kul.Insert(new Kullanici()
                {
                     Email=model.Email,
                     KullaniciAdi=model.KullaniciAdi,
                     Sifre=model.Sifre,
                     AktifGuid=Guid.NewGuid(),
                     Admin=false,
                     Aktif=false                
                });
                if(kaydet>0)
                {
     sonuc.nesne = rep_kul.Find(x=>x.Email==model.Email && x.KullaniciAdi==model.KullaniciAdi);

                    //Aktivasyon maili gönderilecek
                    string siteUrl = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUrl = $"{siteUrl}/Home/UserActivate/{sonuc.nesne.AktifGuid}";
                    string body = $"Merhaba {sonuc.nesne.KullaniciAdi} <br/> Hesabınızı aktifleştirmek için <a href='{activateUrl}'> tıklayınız</a> ";
                 

                    MailHelper.SendMail(body, sonuc.nesne.Email, "Hesap Aktifleştirme");

                }
               
            }
            return sonuc;
        }
    
        public BusinessLayerSonuc<Kullanici> LoginKontrol(LoginModel model)
        {
            BusinessLayerSonuc<Kullanici> sonuc = new BusinessLayerSonuc<Kullanici>();

            sonuc.nesne = rep_kul.Find(x => x.KullaniciAdi == model.KullaniciAdi && x.Sifre == model.Sifre);

            if (sonuc.nesne != null)
            {
                if(!sonuc.nesne.Aktif)
                {
                    sonuc.Hatalar.Add("Kullanıcı aktif değildir.Aktivasyon için e-posta adresiniz kontrol ediniz.");
                }
            }
            else
            {
                sonuc.Hatalar.Add("Kullanıcı adı ve şifre eşleşmiyor.");
            }
            return sonuc;

        }
    }
}
