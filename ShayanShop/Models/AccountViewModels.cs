using System.ComponentModel.DataAnnotations;

namespace ShayanShop.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "لطفا مقدار {0} را وارد نمایید")]
        [MaxLength(300)]
        [EmailAddress(ErrorMessage = "مقدار وارد شده برای {0} شبیه یک آدرس ایمیل نیست")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا مقدار {0} را وارد نمایید")]
        [StringLength(100, ErrorMessage = "طول {0} میبایست بین {2} تا {1} کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفا مقدار {0} را وارد نمایید")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "تایید رمز عبور با رمز عبور یکسان نیست")]
        [Display(Name = "تکرار رمز عبور")]
        public string RePassword { get; set; }
    }



    public class LoginViewModel
    {
        [Required(ErrorMessage = "لطفا مقدار {0} را وارد نمایید")]
        [MaxLength(300)]
        [EmailAddress(ErrorMessage = "مقدار وارد شده برای {0} شبیه یک آدرس ایمیل نیست")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        

        [Required(ErrorMessage = "لطفا مقدار {0} را وارد نمایید")]
        [StringLength(100, ErrorMessage = "طول {0} میبایست بین {2} تا {1} کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }


        [Display(Name = "مرا بخاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}
