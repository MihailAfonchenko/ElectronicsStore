using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _AM_ElectronicsStore.UI.Web.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [StringLength(1000)]
        [AllowHtml]
        [Display(Name = "Введите старый пароль")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(1000)]
        [AllowHtml]
        [Display(Name = "Введите новый пароль")]
        public string NewPassword { get; set; }

        [Required]
        [StringLength(1000)]
        [AllowHtml]
        [Display(Name = "Введите повторно новый пароль")]
        [System.Web.Mvc.Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        public string NewPasswordConfirm { get; set; }
    }
}