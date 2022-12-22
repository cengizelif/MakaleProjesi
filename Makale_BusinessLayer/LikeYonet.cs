using Makale_DataAccessLayer;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BusinessLayer
{
    public class LikeYonet
    {
        Repository<Begeni> rep_like = new Repository<Begeni>();
        public IQueryable<Begeni> ListeleQueryable()
        {
            return rep_like.ListeQueryable();
        }
    }
}
