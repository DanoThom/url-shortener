using System;
using System.Collections.Generic;
using System.Text;
using UrlShortener.Domain.Shared;

namespace UrlShortener.Domain.Aggregates.UrlAggregate
{
    public class Url : Entity, IAggregateRoot
    {
        public string LongUrl { get; private set; }
        public string ShortUrl { get; private set; }
        public DateTimeOffset CreatedOn { get; set; }

        private UrlDetails _urlDetails;
        public UrlDetails UrlDetails => _urlDetails;

        private List<UrlRequest> _urlRequests;
        public IReadOnlyCollection<UrlRequest> UrlRequests => _urlRequests;

        protected Url()
        {
            _urlRequests = new List<UrlRequest>();
        }

        public Url(string longUrl) : this()
        {
            LongUrl = longUrl;
            CreatedOn = DateTimeOffset.UtcNow;
        }

        public void SetShortUrl(string shortUrl) {
            ShortUrl = shortUrl;
            if (_urlDetails == null && Id > 0) {
                _urlDetails = new UrlDetails(Id);
            }
        }

        public void AddNewUrlRequest()
        {
            _urlDetails.IncreateRequestCount();
            //_urlRequests.Add(new UrlRequest(Id));
        }
    }
}
