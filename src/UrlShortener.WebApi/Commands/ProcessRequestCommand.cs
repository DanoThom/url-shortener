using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using UrlShortener.WebApi.Dtos;

namespace UrlShortener.WebApi.Commands {

    [DataContract]
    public class ProcessRequestCommand : IRequest<UrlDto> {

        [DataMember]
        public string ShortUrl { get; private set; }

        public ProcessRequestCommand(string shortUrl) {
            ShortUrl = shortUrl;
        }
    }
}
