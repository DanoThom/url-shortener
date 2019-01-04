using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using UrlShortener.Domain.Aggregates.UrlAggregate;
using UrlShortener.Domain.Shared;
using UrlShortener.Infrastructure.Contexts;

namespace UrlShortener.Infrastructure.Repositories {
    public class UrlRepository : IUrlRepository {

        private readonly UrlShortenerContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public UrlRepository(UrlShortenerContext context) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Url Add(Url url) {
            return _context.Urls.Add(url).Entity;
        }

        public async Task<Url> GetAsync(int urlId) {
            var url = await _context.Urls.FindAsync(urlId);
            if (url != null) {
                await _context.Entry(url).Collection(x => x.UrlRequests).LoadAsync();
                await _context.Entry(url).Reference(x => x.UrlDetails).LoadAsync();
            }

            return url;
        }

        public void Update(Url url) {
            _context.Entry(url).State = EntityState.Modified;
        }
    }
}
