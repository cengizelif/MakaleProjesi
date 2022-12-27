using Makale_DataAccessLayer;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BusinessLayer
{
    public class YorumYonet
    {
        Repository<Yorum> rep_yorum = new Repository<Yorum>();
        public Yorum YorumBul(int id)
        {
            return rep_yorum.Find(x => x.Id == id);
        }

        public int YorumEkle(Yorum yorum)
        {
            return rep_yorum.Insert(yorum);
        }

        public int YorumGuncelle(Yorum yorum)
        {
            return rep_yorum.Update(yorum);
        }

        public int YorumSil(Yorum yorum)
        {
           return  rep_yorum.Delete(yorum);
        }
    }
}
