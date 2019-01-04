using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Domain.Shared;

namespace UrlShortener.Domain.Aggregates.UrlAggregate
{
    public interface IUrlRepository : IRepository<Url>
    {
        Url Add(Url url);
        void Update(Url url);
        Task<Url> GetAsync(int urlId);
    }
}
