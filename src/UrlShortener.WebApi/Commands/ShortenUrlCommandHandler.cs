using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UrlShortener.Domain.Aggregates.UrlAggregate;
using UrlShortener.WebApi.Dtos;
using UrlShortener.WebApi.Exceptions;
using UrlShortener.WebApi.Pages;
using UrlShortener.WebApi.Queries;

namespace UrlShortener.WebApi.Commands {
    public class ShortenUrlCommandHandler : IRequestHandler<ShortenUrlCommand, UrlDto> {
        private readonly IUrlRepository _urlRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ShortenUrlCommandHandler(IUrlRepository urlRepository, IMediator mediator, IMapper mapper) {
            _urlRepository = urlRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<UrlDto> Handle(ShortenUrlCommand request, CancellationToken cancellationToken) {
            // Extra server url validation
            var validUrl = Uri.IsWellFormedUriString(request.Url, UriKind.Absolute);
            if (!validUrl) throw new InvalidUrlException("Please enter a valid url");

            var dbUrl = await _mediator.Send(new GetUrlFromLongUrlQuery(request.Url));

            if(dbUrl != null) {
                return _mapper.Map<UrlDto>(dbUrl);
            }

            var url = new Url(request.Url);

            _urlRepository.Add(url);
            url.SetShortUrl();
            
            await _urlRepository.UnitOfWork.SaveEntitiesAsync();
            return _mapper.Map<UrlDto>(url);
        }
    }
}
