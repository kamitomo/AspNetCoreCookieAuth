using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreCookieAuth.Models
{
    /// <summary>
    /// 更新を追跡するエンティティのインターフェイス
    /// </summary>
    public interface ITrackable
    {
        DateTimeOffset UpdatedAt { get; set; }
        DateTimeOffset CreatedAt { get; set; }
    }
}
