using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;
using UrlShortener.Domain.Aggregates.UrlAggregate;
using UrlShortener.Infrastructure.Services;
using UrlShortener.WebApi.Dtos;
using UrlShortener.WebApi.Exceptions;
using UrlShortener.WebApi.Queries;

namespace UrlShortener.WebApi.Commands {
    public class ShortenUrlCommandHandler : IRequestHandler<ShortenUrlCommand, UrlDto> {
        private readonly IUrlRepository _urlRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IShortUrlService _shortUrlService;
        private readonly IDatabase _database;
        private readonly ILogger<ShortenUrlCommandHandler> _logger;

        public ShortenUrlCommandHandler(IUrlRepository urlRepository, ILoggerFactory loggerFactory, IMediator mediator, IMapper mapper,
            IShortUrlService shortUrlService, ConnectionMultiplexer redis = null) {
            _urlRepository = urlRepository;
            _mediator = mediator;
            _mapper = mapper;
            _shortUrlService = shortUrlService;
            _database = redis?.GetDatabase();
            _logger = loggerFactory.CreateLogger<ShortenUrlCommandHandler>();
        }

        public async Task<UrlDto> Handle(ShortenUrlCommand request, CancellationToken cancellationToken) {
            var longUrl = ValidateUrl(request);

            var shortUrl = _shortUrlService.GetShortUrl(longUrl);

            // check cache
            UrlDto urlDto = null;
            if (_database != null) {
                var data = await _database.StringGetAsync(shortUrl);
                if (!data.IsNullOrEmpty) {
                    urlDto = JsonConvert.DeserializeObject<UrlDto>(data);
                    if (urlDto != null) {
                        urlDto.LongUrl = GetResultLongUrl(urlDto.LongUrl);
                        return urlDto;
                    }
                }
            }

            var dbUrl = await _mediator.Send(new GetUrlFromLongUrlQuery(longUrl));

            if (dbUrl != null) {
                var dbUrlDto = _mapper.Map<UrlDto>(dbUrl);
                dbUrlDto.LongUrl = GetResultLongUrl(dbUrl.LongUrl);
                return dbUrlDto;
            }

            var url = new Url(longUrl);
            _urlRepository.Add(url);

            url.SetShortUrl(shortUrl);

            await _urlRepository.UnitOfWork.SaveEntitiesAsync();
            urlDto = _mapper.Map<UrlDto>(url);

            if (_database != null) {
                var created = await _database.StringSetAsync(urlDto.ShortUrl, JsonConvert.SerializeObject(urlDto));
                if (!created) {
                    _logger.LogInformation("Error when setting cache for url!");
                    return null;
                }

                _logger.LogInformation("Url cache updated successfully.");
            }

            urlDto.LongUrl = GetResultLongUrl(urlDto.LongUrl);
            return urlDto;
        }

        private string ValidateUrl(ShortenUrlCommand request) {
            var validUrl = Uri.IsWellFormedUriString(request.Url, UriKind.Absolute);
            bool result = Uri.TryCreate(request.Url, UriKind.Absolute, out Uri uri) && (uri.Scheme == "http" || uri.Scheme == "https");
            if (!validUrl || !result) throw new InvalidUrlException("Please enter a valid url");

            // clean incoming long url
            var longUrl = uri.Host.ToLower().Replace("www.", "") + "/" + (string.IsNullOrWhiteSpace(uri.Fragment) ? uri.PathAndQuery : uri.Fragment);
            return longUrl;
        }

        private string GetResultLongUrl(string url) {
            return "https://" + url;
        }
    }
}
