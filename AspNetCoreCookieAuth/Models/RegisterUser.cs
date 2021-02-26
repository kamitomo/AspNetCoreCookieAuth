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
        [Required(ErrorMessage = "{0} の入力が必要です。")]
        [MaxLength(30, ErrorMessage = "{0} の最大文字数は {1} です。")]
        [RegularExpression(@"[a-zA-Z0-9\.\-_]+", ErrorMessage = "{0} に使えるのは、半角英数字、ドット(.)、ハイフン(-)、アンダースコア(_)のみです。")]
        [Display(Name = "ユーザー名")]
        public string UserId { get; set; }

        /// <summary>
        /// パスワード
        /// </summary>
        [Required(ErrorMessage = "{0} の入力が必要です。")]
        [MaxLength(100, ErrorMessage = "{0} の最大文字数は {1} です。"), MinLength(6, ErrorMessage = "{0} の最小文字数は {1} です。")]
        [RegularExpression(@"[!-~]+", ErrorMessage = "{0} に使えるのは、半角英数字記号のみです。")]
        [Display(Name = "パスワード")]
        public string Password { get; set; }

        /// <summary>
        /// 確認用パスワード
        /// </summary>
        [Required(ErrorMessage = "{0} の入力が必要です。")]
        [MaxLength(100, ErrorMessage = "{0} の最大文字数は {1} です。"), MinLength(6, ErrorMessage = "{0} の最小文字数は {1} です。")]
        [RegularExpression(@"[!-~]+", ErrorMessage = "{0} に使えるのは、半角英数字記号のみです。")]
        [Display(Name = "確認用パスワード")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// 管理者
        /// </summary>
        [Display(Name = "管理者")]
        public bool Admin { get; set; }
    }
}
