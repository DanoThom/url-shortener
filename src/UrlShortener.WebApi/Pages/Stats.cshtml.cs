using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UrlShortener.Domain.Aggregates.UrlAggregate;
using UrlShortener.WebApi.Dtos;
using UrlShortener.WebApi.Queries;

namespace UrlShortener.WebApi.Pages {
    public class ChartData {
        public int Count { get; set; }
        public string Date { get; set; }
    }

    public class StatsModel : PageModel {
        private readonly IUrlRepository _urlRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        //[BindProperty]
        public UrlDto UrlModel { get; set; }

        public List<ChartData> ChartData { get; set; }

        public StatsModel(IUrlRepository urlRepository, IMediator mediator, IMapper mapper) {
            _urlRepository = urlRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task OnGet([FromRoute] string shortUrl) {
            var url = await _urlRepository.GetAsync(shortUrl);
            var urlRequests = await _mediator.Send(new GetUrlRequestsQuery(url.Id));

            ChartData = urlRequests.ToList()
                .GroupBy(s => new { date = s.AccessedOn.Date })
                .OrderBy(s => s.Key.date)
                .Select(x => new ChartData { Count = x.Count(), Date = x.Key.date.ToShortDateString() }).ToList();

            UrlModel = _mapper.Map<UrlDto>(url);
        }

        
    }
}