using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreCookieAuth.Models
{
    public class RegisterUser
    {
        /// <summary>
        /// ユーザ ID
        /// </summary>
        [Required]
        [MaxLength(30)]
        [Display(Name = "ユーザー名")]
        public string UserId { get; set; }

        /// <summary>
        /// パスワード
        /// </summary>
        [Required]
        [MaxLength(100), MinLength(6)]
        [Display(Name = "パスワード")]
        public string Password { get; set; }

        /// <summary>
        /// 確認用パスワード
        /// </summary>
        [Required]
        [MaxLength(100), MinLength(6)]
        [Display(Name = "確認用パスワード")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// 管理者
        /// </summary>
        [Display(Name = "管理者")]
        public bool Admin { get; set; }
    }
}
