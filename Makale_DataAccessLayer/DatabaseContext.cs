using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_DataAccessLayer
{
    public class DatabaseContext:DbContext
    {
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Yorum> Yorumlar { get; set; }
        public DbSet<Not> Notlar { get; set;}
        public DbSet<Begeni> Begeniler { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new VeritabaniOlusturucu());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //FluentApi
            modelBuilder.Entity<Not>().HasMany(n => n.Yorumlar).WithRequired(y => y.Not).WillCascadeOnDelete(true);

            modelBuilder.Entity<Not>().HasMany(n => n.Begeniler).WithRequired(b => b.Not).WillCascadeOnDelete(true);
        }
    }
}
