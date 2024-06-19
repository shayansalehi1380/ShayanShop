using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShayanShop.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "نام کاربری")]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }
        [Display(Name = "آیا کاربر ادمین است؟")]
        public bool IsAdmin { get; set; }

        public List<Order> Orders { get; set; }
    }
}
