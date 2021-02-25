using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreCookieAuth.Models
{
    public class UserListItem
    {
        /// <summary>
        /// ユーザ ID
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string UserId { get; set; }

        /// <summary>
        /// 管理者
        /// </summary>
        public bool Admin { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
