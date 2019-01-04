using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UrlShortener.Domain.Aggregates.UrlAggregate;
using UrlShortener.Infrastructure.Contexts;

namespace UrlShortener.WebApi.Queries {
    public class GetUrlFromLongUrlQueryHandler : IRequestHandler<GetUrlFromLongUrlQuery, Url> {
        private readonly UrlShortenerContext _context;

        public GetUrlFromLongUrlQueryHandler(UrlShortenerContext context) {
            _context = context;
        }

        public async Task<Url> Handle(GetUrlFromLongUrlQuery request, CancellationToken cancellationToken) {
            var url = await _context.Urls.SingleOrDefaultAsync(x => x.LongUrl.Equals(request.LongUrl));

            return url;
        }
    }
}
