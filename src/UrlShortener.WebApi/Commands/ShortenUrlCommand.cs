﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using UrlShortener.WebApi.Dtos;

namespace UrlShortener.WebApi.Commands {

    [DataContract]
    public class ShortenUrlCommand : IRequest<UrlDto> {

        [DataMember]
        public string Url { get; private set; }

        public ShortenUrlCommand(string url) {
            Url = url;
        }
    }
}
