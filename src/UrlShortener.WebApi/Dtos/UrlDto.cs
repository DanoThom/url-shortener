using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.WebApi.Dtos {
    public class UrlDto {
        public int Id { get; set; }
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public int RequestCount { get; set; }
    }
}
