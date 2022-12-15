using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities.ViewModel
{
    public class KayitModel
    {
        [DisplayName("Kullanıcı Adı"),Required(ErrorMessage ="{0} alanı boş geçilemez"), StringLength(20)]
        public string KullaniciAdi { get; set; }

        [DisplayName("E-posta"),Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(50),EmailAddress(ErrorMessage ="Geçerli bir email adres giriniz")]
        public string Email { get; set; }

        [DisplayName("Şifre"),Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(20,ErrorMessage ="{0} en fazla 20 karakter olmalıdır")]
        public string Sifre { get; set; }

        [DisplayName("Şifre(Tekrar)"),Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(20,ErrorMessage = "{0} en fazla 20 karakter olmalıdır"),Compare(nameof(Sifre),ErrorMessage ="{0} ile {1} uyuşmuyor")]
        public string Sifre2 { get; set; }
    }
}
