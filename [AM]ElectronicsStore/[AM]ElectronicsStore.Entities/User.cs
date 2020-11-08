using _AM_ElectronicsStore.Entities.RoleUser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _AM_ElectronicsStore.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Логин")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина логина должна составлять от 3 до 50 символов")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [StringLength(1000)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Повторите пароль")]
        [DataType(DataType.Password)]
        [StringLength(1000)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "EMail")]
        [StringLength(1000)]
        public string Mail { get; set; }

        [Display(Name = "Дата регистрации")]
        public DateTime DateRegistration { get; set; }

        [Display(Name = "Администратор")]
        public RoleType RoleId { get; set; }

        private static SHA512 hashAlgorithm = SHA512.Create();

        public static bool VerifyPassword(string psw, string pswCheck)
        {
            return psw == GetStringHash(pswCheck);
        }

        /// <summary>
        /// Получение SHA512-хеша
        /// </summary>
        /// <param name="psw"></param>
        /// <returns></returns>
        public static string GetStringHash(string psw)
        {
            if (psw == null)
                return null;
            var hash = hashAlgorithm.ComputeHash(Encoding.Unicode.GetBytes(psw));
            return string.Join("", hash.Select(item => item.ToString("x2")));
        }
    }
}
