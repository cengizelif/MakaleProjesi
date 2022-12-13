using Makale_DataAccessLayer;
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
            DatabaseContext db = new DatabaseContext();
            //db.Database.CreateIfNotExists();
            db.Kategoriler.ToList();
        }
    }
}
