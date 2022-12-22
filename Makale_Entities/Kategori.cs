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
    [Table("Kategori")]
    public class Kategori:EntitiesBase
    {
        [DisplayName("Kategori"),Required,StringLength(50)]
        public string Baslik { get; set; }

        [StringLength(150)]
        public string Aciklama { get; set; } 

        public virtual List<Not> Notlar { get; set; }

        public Kategori()
        {
            Notlar = new List<Not>();
        }
    }
}
