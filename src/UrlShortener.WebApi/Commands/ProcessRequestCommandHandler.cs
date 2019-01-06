using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UrlShortener.Domain.Aggregates.UrlAggregate;
using UrlShortener.WebApi.Dtos;
using UrlShortener.WebApi.Exceptions;

namespace UrlShortener.WebApi.Commands {
    public class ProcessRequestCommandHandler : IRequestHandler<ProcessRequestCommand, UrlDto> {
        private readonly IUrlRepository _urlRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProcessRequestCommandHandler(IUrlRepository urlRepository, IMediator mediator, IMapper mapper) {
            _urlRepository = urlRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<UrlDto> Handle(ProcessRequestCommand request, CancellationToken cancellationToken) {
            var url = await _urlRepository.GetAsync(request.ShortUrl);
            if (url == null || url.Id == 0) {
                throw new UrlNotFoundException($"No url found for short url '{request.ShortUrl}'");
            }

            url.AddNewUrlRequest();

            _urlRepository.Update(url);

            await _urlRepository.UnitOfWork.SaveEntitiesAsync();

            var urlDto = _mapper.Map<UrlDto>(url);
            urlDto.LongUrl = "https://" + urlDto.LongUrl;
            return urlDto;
        }
    }
}
