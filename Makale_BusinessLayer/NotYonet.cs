using Makale_DataAccessLayer;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BusinessLayer
{
    public class NotYonet
    {
        Repository<Not> rep_not = new Repository<Not>();
        BusinessLayerSonuc<Not> notsonuc = new BusinessLayerSonuc<Not>();
        public List<Not> Listele()
        {
            return rep_not.Liste();
        }

        public IQueryable<Not> ListeleQueryable()
        {
            return rep_not.ListeQueryable();
        }

        public Not NotBul(int id)
        {
            return rep_not.Find(x => x.Id == id);
        }

        public BusinessLayerSonuc<Not> NotKaydet(Not not)
        {
            notsonuc.nesne = rep_not.Find(x => x.Baslik == not.Baslik && x.KategoriId == not.KategoriId);

            if(notsonuc.nesne != null)
            {
                notsonuc.Hatalar.Add("Bu kategoride bu isimde makale kayıtlıdır");
            }
            else
            {
               int sonuc=rep_not.Insert(not);
                if (sonuc < 1)
                    notsonuc.Hatalar.Add("Kayıt eklenemedi");
            }
            return notsonuc;
        }

        public BusinessLayerSonuc<Not> NotSil(Not not)
        {
            notsonuc.nesne = rep_not.Find(x => x.Id == not.Id);
            if(notsonuc.nesne!=null)
            {
                int sonuc = rep_not.Delete(not);
                if (sonuc < 1)
                    notsonuc.Hatalar.Add("Makale silinemedi");
            }
            else
            {
                notsonuc.Hatalar.Add("Makale bulunamadı");
            }
            return notsonuc;
        }

        public BusinessLayerSonuc<Not> NotUpdate(Not not)
        {
            notsonuc.nesne=rep_not.Find(x => x.Id == not.Id);

            if(notsonuc.nesne!=null)
            {
                notsonuc.nesne.Baslik = not.Baslik;
                notsonuc.nesne.Text = not.Text;
                notsonuc.nesne.Taslak = not.Taslak;
                notsonuc.nesne.KategoriId = not.KategoriId;
                notsonuc.nesne.BegeniSayisi = not.BegeniSayisi;
               
                if(rep_not.Update(notsonuc.nesne)<1)
                {
                    notsonuc.Hatalar.Add("Kayıt güncellenemedi");
                }
            }
            return notsonuc;
        }
    }
}
