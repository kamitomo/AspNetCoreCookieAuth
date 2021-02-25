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
        public string UserId { get; set; }

        /// <summary>
        /// パスワード
        /// </summary>
        [Required]
        [MaxLength(100), MinLength(6)]
        public string Password { get; set; }

        /// <summary>
        /// 確認用パスワード
        /// </summary>
        [Required]
        [MaxLength(100), MinLength(6)]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// 管理者
        /// </summary>
        public bool Admin { get; set; }
    }
}
