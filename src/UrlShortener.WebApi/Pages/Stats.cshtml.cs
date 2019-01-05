using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UrlShortener.Domain.Aggregates.UrlAggregate;
using UrlShortener.WebApi.Dtos;

namespace UrlShortener.WebApi.Pages {
    public class StatsModel : PageModel {
        private readonly IUrlRepository _urlRepository;
        private readonly IMapper _mapper;

        //[BindProperty]
        public UrlDto UrlModel { get; set; }

        public StatsModel(IUrlRepository urlRepository, IMapper mapper) {
            _urlRepository = urlRepository;
            _mapper = mapper;
        }

        public async Task OnGet([FromRoute] string shortUrl) {
            var url = await _urlRepository.GetAsync(shortUrl);
            UrlModel = _mapper.Map<UrlDto>(url);
        }
    }
}