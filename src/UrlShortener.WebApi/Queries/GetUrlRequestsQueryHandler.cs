using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using UrlShortener.WebApi.Dtos;

namespace UrlShortener.WebApi.Queries {
    public class GetUrlRequestsQueryHandler : IRequestHandler<GetUrlRequestsQuery, IEnumerable<UrlRequestDto>> {
        private readonly IConfiguration _config;

        public GetUrlRequestsQueryHandler(IConfiguration config) {
            _config = config;
        }

        public async Task<IEnumerable<UrlRequestDto>> Handle(GetUrlRequestsQuery request, CancellationToken cancellationToken) {
            using (var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"))) {

                var urlRequests = await conn.QueryAsync<UrlRequestDto>(
                    "select * from core.UrlRequest where UrlId = @id", new { id = request.UrlId });

                return urlRequests;
            }
        }
    }
}
