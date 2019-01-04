namespace UrlShortener.Infrastructure.Services {

    public interface IShortUrlService {
        string GetShortUrl(string longUrl);
    }

}