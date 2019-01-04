using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.WebApi.Commands;
using UrlShortener.WebApi.Exceptions;

namespace UrlShortener.WebApi.Controllers {

    [ApiController]
    public class UrlsController : ControllerBase {
        private readonly IMediator _mediator;

        public UrlsController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("/{shortUrl}")]
        public async Task<IActionResult> RedirectShortUrl(string shortUrl) {
            try {
                var url = await _mediator.Send(new ProcessRequestCommand(shortUrl));
                return Redirect(url.LongUrl);
            }
            catch (UrlNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch(Exception ex) {
                return BadRequest(ex);
            }
        }
        
        [HttpPost]
        [Route("api/shortenUrl")]
        public async Task<IActionResult> ShortenUrl([FromBody] ShortenUrlCommand command) {
            try {
                return Ok(await _mediator.Send(command));
            }
            catch (InvalidUrlException ex) {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) {
                return BadRequest(ex);
            }
        }
    }
}
