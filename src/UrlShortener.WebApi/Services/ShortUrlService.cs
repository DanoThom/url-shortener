using Base62;
using System.Security.Cryptography;
using System.Text;

namespace UrlShortener.Infrastructure.Services {
    public class ShortUrlService : IShortUrlService {
        private const int SHORTURLLENGTH = 8;

        public string GetShortUrl(string longUrl) {
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(longUrl));
            return bytes.ToBase62().Substring(0, SHORTURLLENGTH);
        }
    }
}
