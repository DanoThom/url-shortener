using MediatR;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UrlShortener.WebApi.Dtos;

namespace UrlShortener.WebApi.Queries {

    [DataContract]
    public class GetUrlRequestsQuery : IRequest<IEnumerable<UrlRequestDto>> {

        [DataMember]
        public int UrlId { get; private set; }

        public GetUrlRequestsQuery(int urlId) {
            UrlId = urlId;
        }
    }
}
