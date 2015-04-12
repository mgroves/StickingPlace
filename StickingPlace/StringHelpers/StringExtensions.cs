using System;
using System.Security.Cryptography;
using System.Text;

namespace StickingPlace
{
    public static class StringExtensions
    {
        public static string ToMD5(this string @this)
        {
            if (string.IsNullOrEmpty(@this))
                throw new ArgumentException("String must not be null or empty.");

            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(@this);
            var hash = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("x2"));
            return sb.ToString();
        }

        public static string ToSHA256(this string @this)
        {
            if (string.IsNullOrEmpty(@this))
                return @this;
            using (var hash = new SHA256Managed())
            {
                var asBytes = Encoding.UTF8.GetBytes(@this);
                var result = hash.ComputeHash(asBytes);
                var resultString = BitConverter.ToString(result);
                var resultStringLowerCaseNoDashes = resultString.Replace("-", "").ToLower();
                return resultStringLowerCaseNoDashes;
            }
        }
    }
}