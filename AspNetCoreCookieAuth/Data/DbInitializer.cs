using AspNetCoreCookieAuth.Auth;
using AspNetCoreCookieAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreCookieAuth.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            PasswordHasher hasher = new PasswordHasher();
            (string hashedPassword, byte[] salt) = hasher.HashPassword("dFhJAmM-DGc8Wjt4WM7h");

            User user = new User { UserId = "demouser", HashedPassword = hashedPassword, Salt = salt };

            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
