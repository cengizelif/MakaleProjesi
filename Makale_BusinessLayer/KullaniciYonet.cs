using Makale_Common;
using Makale_DataAccessLayer;
using Makale_Entities;
using Makale_Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BusinessLayer
{
    public class KullaniciYonet
    {
        Repository<Kullanici> rep_kul = new Repository<Kullanici>();

        BusinessLayerSonuc<Kullanici> sonuc = new BusinessLayerSonuc<Kullanici>();

        public List<Kullanici> Listele()
        {
            return rep_kul.Liste();
        }

        public BusinessLayerSonuc<Kullanici> ActivateUser(Guid id)
        {
            BusinessLayerSonuc<Kullanici> sonuc = new BusinessLayerSonuc<Kullanici>();

            sonuc.nesne = rep_kul.Find(x => x.AktifGuid == id);
           
            if(sonuc.nesne!=null)
            {
                if(sonuc.nesne.Aktif)
                {
                    sonuc.Hatalar.Add("Kullanıcı zaten aktif edilmiştir");
                    return sonuc;
                }
                sonuc.nesne.Aktif = true;
                rep_kul.Update(sonuc.nesne);
            }
            else
            {
                sonuc.Hatalar.Add("Aktifleştirilecek kullanıcı bulunamadı");
            }

            return sonuc;

        }

  
        public BusinessLayerSonuc<Kullanici> Kaydet(KayitModel model)
        {
            Kullanici k = new Kullanici();
            k.KullaniciAdi = model.KullaniciAdi;
            k.Email = model.Email;

            sonuc = KullaniciKontrol(k);

            if (sonuc.Hatalar.Count > 0)
            {
                sonuc.nesne = k;
                return sonuc;
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
    
        public BusinessLayerSonuc<Kullanici> KullaniciUpdate(Kullanici kullanici)
        {
            sonuc = KullaniciKontrol(kullanici);

            if (sonuc.Hatalar.Count > 0)
            {
                sonuc.nesne = kullanici;
                return sonuc;
            }

            sonuc.nesne = rep_kul.Find(x => x.Id == kullanici.Id);
            sonuc.nesne.Ad = kullanici.Ad;
            sonuc.nesne.Soyad = kullanici.Soyad;
            sonuc.nesne.KullaniciAdi = kullanici.KullaniciAdi;
            sonuc.nesne.Email = kullanici.Email;
            sonuc.nesne.Sifre = kullanici.Sifre;

            if (!string.IsNullOrEmpty(kullanici.ProfilResim))
                sonuc.nesne.ProfilResim = kullanici.ProfilResim;

            int updatesonuc = rep_kul.Update(sonuc.nesne);

            if (updatesonuc < 1)
            {
                sonuc.Hatalar.Add("Profil güncellenemedi");
            }
            return sonuc;
        }

        private BusinessLayerSonuc<Kullanici> KullaniciKontrol(Kullanici kullanici)
        {
            Kullanici k1 = rep_kul.Find(x => x.KullaniciAdi == kullanici.KullaniciAdi);

            Kullanici k2 = rep_kul.Find(x => x.Email == kullanici.Email);

            if (k1 != null && k1.Id != kullanici.Id)
            {
                sonuc.Hatalar.Add("Kullanıcı adı sistemde kayıtlı");
            }

            if (k2 != null && k2.Id != kullanici.Id)
            {                            
                sonuc.Hatalar.Add("Email sistemde kayıtlı");
            }

            return sonuc;
        }

        public BusinessLayerSonuc<Kullanici> KullaniciSil(int id)
        {
            sonuc.nesne = rep_kul.Find(x => x.Id == id);
            if(sonuc.nesne!=null)
            {
               int silsonuc= rep_kul.Delete(sonuc.nesne);
                if (silsonuc < 1)
                    sonuc.Hatalar.Add("Kullanıcı silinemedi.");
            }
            else
            {
                sonuc.Hatalar.Add("Kullanıcı bulunamadı");
            }
            return sonuc;
        }

        public Kullanici KullanciBul(int id)
        {
          return rep_kul.Find(x => x.Id == id);
        }

        public BusinessLayerSonuc<Kullanici> KullaniciKaydet(Kullanici kullanici)
        {
            sonuc = KullaniciKontrol(kullanici);

            if (sonuc.Hatalar.Count > 0)
            {
                sonuc.nesne = kullanici;
                return sonuc;
            }
            else
            {
                int kayit = rep_kul.Insert(kullanici);
                if (kayit < 1)
                {
                    sonuc.Hatalar.Add("Kategori kaydedilemedi.");
                }
            }
            return sonuc;
        }
    }
}
