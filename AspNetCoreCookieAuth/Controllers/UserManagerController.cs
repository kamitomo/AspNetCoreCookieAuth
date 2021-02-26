using AspNetCoreCookieAuth.Auth;
using AspNetCoreCookieAuth.Data;
using AspNetCoreCookieAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreCookieAuth.Controllers
{
    [Authorize(Policy = "Admin")]
    public class UserManagerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserManagerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users
                .AsNoTracking()
                .Select(user =>
                    new UserListItem
                    {
                        UserId = user.UserId,
                        Admin = user.Admin,
                        CreatedAt = user.CreatedAt,
                        UpdatedAt = user.UpdatedAt,
                    }
                );
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterUser model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var registered = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.UserId == model.UserId);
            
            if (registered != null)
            {
                ModelState.AddModelError(string.Empty, "すでにこのユーザー名は使用されています。");
                return View(model);
            }

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "確認用パスワードが一致しません。");
                return View(model);
            }

            #region 新規ユーザ登録処理

            // ハッシュ化パスワードとソルトを作成
            PasswordHasher hasher = new PasswordHasher();
            (string hashedPassword, byte[] salt) = hasher.HashPassword(model.Password);

            // ユーザを新規作成
            User user = new User { UserId = model.UserId, HashedPassword = hashedPassword, Salt = salt, Admin = false };

            // ユーザテーブルに追加
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            #endregion

            return RedirectToAction(nameof(Index));
        }
    }
}
