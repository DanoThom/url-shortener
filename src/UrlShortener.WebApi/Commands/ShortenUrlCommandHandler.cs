using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UrlShortener.Domain.Aggregates.UrlAggregate;
using UrlShortener.WebApi.Pages;

namespace UrlShortener.WebApi.Commands {
    public class ShortenUrlCommandHandler : IRequestHandler<ShortenUrlCommand, Pages.IndexModel> {
        private readonly IUrlRepository _urlRepository;

        public ShortenUrlCommandHandler(IUrlRepository urlRepository) {
            _urlRepository = urlRepository;
        }

        public async Task<IndexModel> Handle(ShortenUrlCommand request, CancellationToken cancellationToken) {
            var url = new Url(request.Url);

            _urlRepository.Add(url);
            url.SetShortUrl();
            
            await _urlRepository.UnitOfWork.SaveEntitiesAsync();
            return Pages.IndexModel.FromUrl(url);
        }
    }
}
