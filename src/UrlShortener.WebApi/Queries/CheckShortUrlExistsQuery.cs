using MediatR;
using System.Runtime.Serialization;

namespace UrlShortener.WebApi.Queries {

    [DataContract]
    public class CheckShortUrlExistsQuery : IRequest<bool> {

        [DataMember]
        public string ShortUrl { get; private set; }

        public CheckShortUrlExistsQuery(string shortUrl) {
            ShortUrl = shortUrl;
        }
    }
}
