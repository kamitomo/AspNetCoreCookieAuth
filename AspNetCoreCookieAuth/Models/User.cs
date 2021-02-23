using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreCookieAuth.Models
{
    /// <summary>
    /// ユーザ
    /// </summary>
    public class User : ITrackable
    {
        /// <summary>
        /// ユーザ ID
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string UserId { get; set; }

        /// <summary>
        /// ハッシュ化パスワード
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string HashedPassword { get; set; }
        
        /// <summary>
        /// ソルト
        /// </summary>
        [Required]
        public byte[] Salt { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }
        
        [Required]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
