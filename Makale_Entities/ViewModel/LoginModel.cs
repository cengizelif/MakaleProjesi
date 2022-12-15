using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities.ViewModel
{
    public class LoginModel
    {
        [DisplayName("Kullanıcı Adı"),Required(ErrorMessage ="{0} alanı boş geçilemez"), StringLength(20)]
        public string KullaniciAdi { get; set; }

        [DisplayName("Şifre"),Required(ErrorMessage ="{0} alanı boş geçilemez"), StringLength(20)]
        public string Sifre { get; set; }
    }
}
