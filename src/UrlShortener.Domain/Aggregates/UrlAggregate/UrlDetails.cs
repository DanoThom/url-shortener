using System;
using System.Collections.Generic;
using System.Text;
using UrlShortener.Domain.Shared;

namespace UrlShortener.Domain.Aggregates.UrlAggregate
{
    public class UrlDetails : Entity
    {
        public int UrlId { get; private set; }
        public int RequestCount { get; private set; }

        private Url _url;
        public Url Url => _url;

        public void IncreateRequestCount() => RequestCount++;

        public UrlDetails(int urlId)
        {
            UrlId = urlId;
        }
    }
}
