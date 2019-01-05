using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.WebApi.Dtos {
    public class UrlRequestDto {
        public int Id { get; set; }
        public int UrlId { get; set; }
        public DateTimeOffset AccessedOn { get; set; }
    }
}
