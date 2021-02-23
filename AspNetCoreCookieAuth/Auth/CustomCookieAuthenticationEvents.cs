using AspNetCoreCookieAuth.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreCookieAuth.Auth
{
    /// <summary>
    /// クッキー認証イベント
    /// ユーザデータベースの変更を検知して無効な場合はサインアウト
    /// </summary>
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomCookieAuthenticationEvents(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var userPrincipal = context.Principal;

            // 認証クッキーから最終変更時刻を取得
            var lastChanged = (from c in userPrincipal.Claims
                               where c.Type == "LastChanged"
                               select c.Value).FirstOrDefault();

            if (string.IsNullOrEmpty(lastChanged) ||
                !this.ValidateLastChanged(userPrincipal.Identity.Name, lastChanged))
            {
                // 変更されていたら認証クッキーを削除してサインアウト
                context.RejectPrincipal();

                await context.HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }

        private bool ValidateLastChanged(string userId, string lastChanged)
        {
            var user = _dbContext.Users.Find(userId);
            if (user == null)
            {
                return false;
            }

            var time = DateTimeOffset.Parse(lastChanged);

            // ユーザデータベースの更新時刻と比較
            return DateTimeOffset.Compare(time, user.UpdatedAt) <= 0;
        }
    }
}
