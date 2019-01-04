using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace UrlShortener.WebApi.Commands {

    [DataContract]
    public class ShortenUrlCommand : IRequest<Pages.IndexModel> {

        [DataMember]
        public string Url { get; private set; }

        public ShortenUrlCommand(string url) {
            Url = url;
        }
    }
}
