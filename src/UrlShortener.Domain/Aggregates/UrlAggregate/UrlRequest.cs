using System;
using System.Collections.Generic;
using System.Text;
using UrlShortener.Domain.Shared;

namespace UrlShortener.Domain.Aggregates.UrlAggregate
{
    public class UrlRequest : Entity
    {
        public int UrlId { get; private set; }
        public DateTimeOffset AccessedOn { get; private set; }

        public UrlRequest(int urlId)
        {
            UrlId = urlId;
            AccessedOn = DateTimeOffset.UtcNow;
        }
    }
}
