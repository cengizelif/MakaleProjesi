using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities
{
    [Table("Notlar")]
    public class Not:EntitiesBase
    {
        [Required,StringLength(50)]
        public string Baslik { get; set; }

        [Required, StringLength(250)]
        public string Text { get; set; }
        public bool Taslak { get; set; }
        public int BegeniSayisi { get; set; }

        [DisplayName("Kategori")]
        public int KategoriId { get; set; } 

        public virtual Kategori Kategori { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        public virtual List<Yorum> Yorumlar { get; set; }
        public virtual List<Begeni> Begeniler { get; set; }


        public Not()
        {
            Yorumlar = new List<Yorum>();
            Begeniler = new List<Begeni>();
        }
    }
}
