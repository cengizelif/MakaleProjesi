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
    }
}
