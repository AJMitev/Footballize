namespace Footballize.Common
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    builder.Append(data[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public static bool TryValidatePassword(string inputPassword, string hashedPassword)
        {
            var hashedInput = PasswordHelper.HashPassword(inputPassword);
            var comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashedInput, hashedPassword) == 0;
        }
    }
}