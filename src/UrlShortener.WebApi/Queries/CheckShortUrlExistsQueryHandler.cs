using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace UrlShortener.WebApi.Queries {
    public class CheckShortUrlExistsQueryHandler : IRequestHandler<CheckShortUrlExistsQuery, bool> {
        private readonly IConfiguration _config;

        public CheckShortUrlExistsQueryHandler(IConfiguration config) {
            _config = config;
        }
        
        public async Task<bool> Handle(CheckShortUrlExistsQuery request, CancellationToken cancellationToken) {
            using (var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"))) {

                return await conn.ExecuteScalarAsync<bool>(
                    "select 1 where exists (select 1 from core.Url where ShortUrl = @id)", new { id = request.ShortUrl });
            }
        }
    }
}
