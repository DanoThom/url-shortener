using System;
using System.Collections.Generic;
using System.Text;
using UrlShortener.Domain.Aggregates.UrlAggregate;
using UrlShortener.WebApi.Exceptions;
using Xunit;

namespace UrlShortener.UnitTests.Domain {
    public class UrlAggregateTest {
        public UrlAggregateTest() { }

        [Fact]
        public void Create_url_success() {
            var longUrl = "http://google.com";

            var url = new Url(longUrl);

            Assert.NotNull(url);
            Assert.Equal(longUrl, url.LongUrl);
        }

        [Fact]
        public void Create_short_url_success() {
            var urlId = 99;
            var longUrl = "http://google.com";
            var shortUrl = "x77Ui4sA";

            var url = new Url(longUrl);
            url.SetId(urlId);
            url.SetShortUrl(shortUrl);

            Assert.NotNull(url);
            Assert.Equal(shortUrl, url.ShortUrl);
        }

        [Fact]
        public void Set_short_url_should_create_url_detail() {
            var urlId = 99;
            var longUrl = "http://google.com";
            var shortUrl = "x77Ui4sA";

            var url = new Url(longUrl);
            url.SetId(urlId);
            url.SetShortUrl(shortUrl);

            Assert.NotNull(url);
            Assert.NotNull(url.UrlDetails);
        }

        [Fact]
        public void Add_new_url_request_success() {
            var urlId = 99;
            var longUrl = "http://google.com";
            var urlDetails = new UrlDetails(urlId);
            
            var url = new Url(longUrl);
            url.SetUrlDetails(urlDetails);
            url.AddNewUrlRequest();

            Assert.NotNull(url);
            Assert.NotEmpty(url.UrlRequests);
        }
    }
}
