using AspNetCoreCookieAuth.Auth;
using AspNetCoreCookieAuth.Data;
using AspNetCoreCookieAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var users = _context.Users.Select(user =>
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
        public IActionResult Create(RegisterUser model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var registered = _context.Users.Find(model.UserId);
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

            PasswordHasher hasher = new PasswordHasher();
            (string hashedPassword, byte[] salt) = hasher.HashPassword(model.Password);

            User user = new User { UserId = model.UserId, HashedPassword = hashedPassword, Salt = salt, Admin = false };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
