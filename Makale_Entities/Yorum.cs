using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities
{
    [Table("Yorum")]
    public class Yorum:EntitiesBase
    {
        [Required,StringLength(250)]
        public string Text { get; set; }

        public virtual Kullanici Kullanici { get; set; }
        public virtual Not Not { get; set; }
    }
}
