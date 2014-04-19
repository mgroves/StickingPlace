using System;
using System.Security.Cryptography;
using System.Text;

namespace StickingPlace.StringHelpers
{
    public static class StringExtensions
    {
        public static string ToMD5(this string @this)
        {
            if(string.IsNullOrEmpty(@this))
                throw new ArgumentException("String must not be null or empty.");

            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(@this);
            var hash = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("x2"));
            return sb.ToString();
        }         
    }
}