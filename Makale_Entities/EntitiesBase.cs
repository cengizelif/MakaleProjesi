using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities
{
    public class EntitiesBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime KayitTarih { get; set; }

        [Required]
        public DateTime DegistirmeTarihi { get; set; }

        [Required,StringLength(20)]
        public string DegistirenKullanici { get; set; }
    }
}
