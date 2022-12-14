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

        public List<Not> Listele()
        {
            return rep_not.Liste();
        }

        public IQueryable<Not> ListeleQueryable()
        {
            return rep_not.ListeQueryable();
        }
    }
}
