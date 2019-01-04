using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.WebApi.Commands;

namespace UrlShortener.WebApi.Controllers
{
    [ApiController]
    public class UrlsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UrlsController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("api/")]
        public IActionResult GetLongUrl(string shortUrl)
        {
            return RedirectPermanent("https://www.google.com");
        }

        [HttpPost]
        [Route("api/shortenUrl")]
        public async Task<IActionResult> ShortenUrl([FromBody] ShortenUrlCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
