using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreCookieAuth.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0}の入力が必要です。")]
        [Display(Name = "ユーザー名")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "{0}の入力が必要です。")]
        [Display(Name = "パスワード")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
