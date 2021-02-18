﻿using AspNetCoreCookieAuth.Data;
using AspNetCoreCookieAuth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreCookieAuth.Controllers
{
    public class AccountController : Controller
    {
		private readonly ApplicationDbContext _context;

		public AccountController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Login()
        {
            return View();
        }

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			// ユーザ検索
			var users = this._context.Users.Where(user => user.UserName == model.UserName);
			if (users.Count() == 0)
            {
				return View(model);
			}
			var user = users.Single();

			// パスワードの確認
			PasswordHasher hasher = new PasswordHasher();
			if (!hasher.VerifyPassword(user.HashedPassword, model.Password, user.Salt))
            {
				return View(model);
			}

			// サインイン用にプリンシパルを作成
			var claims = new[] {
				new Claim(ClaimTypes.Name, user.UserName),
			};
			var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var principal = new ClaimsPrincipal(identity);

			// サインイン
			await HttpContext.SignInAsync(principal);

			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> Logout()
		{
			// サインアウト
			await HttpContext.SignOutAsync();

			return RedirectToAction("Login");
		}
	}
}