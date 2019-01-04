using MediatR;
using System.Runtime.Serialization;
using UrlShortener.Domain.Aggregates.UrlAggregate;

namespace UrlShortener.WebApi.Queries {

    [DataContract]
    public class GetUrlFromLongUrlQuery : IRequest<Url> {

        [DataMember]
        public string LongUrl { get; private set; }

        public GetUrlFromLongUrlQuery(string longUrl) {
            LongUrl = longUrl;
        }
    }
}
