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
    
        public BusinessLayerSonuc<Kullanici> KullaniciUpdate(Kullanici kullanici)
        {
            BusinessLayerSonuc<Kullanici> sonuc = new BusinessLayerSonuc<Kullanici>();

            Kullanici k1 = rep_kul.Find(x => x.KullaniciAdi == kullanici.KullaniciAdi);

            Kullanici k2 = rep_kul.Find(x => x.Email == kullanici.Email);

            if(k1 != null && k1.Id != kullanici.Id)
            {
                //if (k1.KullaniciAdi == kullanici.KullaniciAdi)               
                    sonuc.Hatalar.Add("Kullanıcı adı sistemde kayıtlı");             
            }

            if (k2 != null && k2.Id != kullanici.Id)
            {
               // if (k2.Email == kullanici.Email)                
                    sonuc.Hatalar.Add("Email sistemde kayıtlı");                
            }

            if(sonuc.Hatalar.Count>0)
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

            if(!string.IsNullOrEmpty(kullanici.ProfilResim))
                sonuc.nesne.ProfilResim = kullanici.ProfilResim;

            int updatesonuc=rep_kul.Update(sonuc.nesne);

            if(updatesonuc<1)
            {
                sonuc.Hatalar.Add("Profil güncellenemedi");
            }
            return sonuc;
        }

        public BusinessLayerSonuc<Kullanici> KullaniciSil(int id)
        {
            BusinessLayerSonuc<Kullanici> sonuc = new BusinessLayerSonuc<Kullanici>();
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

        public Kullanici KullanciBul(int value)
        {
            throw new NotImplementedException();
        }

        public void KullaniciKaydet(Kullanici kullanici)
        {
            throw new NotImplementedException();
        }
    }
}
