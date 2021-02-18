using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace AspNetCoreCookieAuth
{
    /// <summary>
    /// パスワード処理のクラス
    /// </summary>
    public class PasswordHasher
    {
        /// <summary>
        /// 平文パスワードをハッシュ化する関数
        /// </summary>
        /// <param name="rawPassword">平文パスワード</param>
        /// <returns>ハッシュ化パスワードとソルト</returns>
        public (string hashedPassword, byte[] salt) HashPassword(string rawPassword)
        {
            byte[] salt = GetSalt();
            string hashedPassword = HashPassword(rawPassword, salt);
            return (hashedPassword, salt);
        }

        /// <summary>
        /// 平文パスワードを確認する関数
        /// </summary>
        /// <param name="hashedPassword">ハッシュ化パスワード</param>
        /// <param name="rawPassword">平文パスワード</param>
        /// <param name="salt">ソルト</param>
        /// <returns>確認できたら true を返す</returns>
        public bool VerifyPassword(string hashedPassword, string rawPassword, byte[] salt)
        {
            return hashedPassword == HashPassword(rawPassword, salt);
        }

        /// <summary>
        /// PBKDF2 を使用してパスワードをハッシュ化する関数
        /// </summary>
        /// <param name="rawPassword">平文パスワード</param>
        /// <param name="salt">ソルト</param>
        /// <returns>ハッシュ化パスワード</returns>
        private string HashPassword(string rawPassword, byte[] salt)
        {
            return Convert.ToBase64String(
              KeyDerivation.Pbkdf2(
                password: rawPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        /// <summary>
        /// ランダムなソルトを生成する関数
        /// </summary>
        /// <returns>ソルト</returns>
        private byte[] GetSalt()
        {
            using (var gen = RandomNumberGenerator.Create())
            {
                var salt = new byte[128 / 8];
                gen.GetBytes(salt);
                return salt;
            }
        }
    }
}
