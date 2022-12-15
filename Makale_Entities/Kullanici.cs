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
    [Table("Kullanici")]
    public class Kullanici:EntitiesBase
    {
        [StringLength(20)]
        public string Ad { get; set; }

        [StringLength(20)]
        public string Soyad { get; set; }

        [Required ,StringLength(20)]
        public string KullaniciAdi { get; set; }

        [Required, StringLength(50)]
        public string Email { get; set; }

        [Required, StringLength(20)]
        public string Sifre { get; set; }
        public bool Aktif { get; set; }
        public bool Admin { get; set; }

        [Required]
        public Guid AktifGuid { get; set; }
        public virtual List<Not> Notlar { get; set; }
        public virtual List<Yorum> Yorumlar { get; set; }
        public virtual List<Begeni> Begeniler { get; set; }

        public Kullanici()
        {
            Notlar = new List<Not>();
            Yorumlar = new List<Yorum>();
            Begeniler = new List<Begeni>();
        }
    }
}
