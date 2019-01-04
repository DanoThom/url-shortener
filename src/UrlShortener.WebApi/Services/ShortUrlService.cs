using Base62;
using System;
using System.Security.Cryptography;
using System.Text;

namespace UrlShortener.Infrastructure.Services {
    public class ShortUrlService : IShortUrlService {
        public string GetShortUrl(string longUrl) {
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(longUrl));

            byte[] truncArray = new byte[6];
            Array.Copy(bytes, truncArray, truncArray.Length);

            return truncArray.ToBase62();
        }
    }
}
