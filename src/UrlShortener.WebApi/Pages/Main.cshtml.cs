using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UrlShortener.Domain.Aggregates.UrlAggregate;

namespace UrlShortener.WebApi.Pages
{
    public class IndexModel : PageModel
    {
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
        public DateTimeOffset CreatedOn { get; set; }

        public void OnGet()
        {

        }

        public static IndexModel FromUrl(Url url) {
            return new IndexModel {
                LongUrl = url.LongUrl,
                ShortUrl = url.ShortUrl,
                CreatedOn = url.CreatedOn
            };
        }
    }
}
