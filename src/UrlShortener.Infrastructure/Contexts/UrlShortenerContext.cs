using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Aggregates.UrlAggregate;
using UrlShortener.Domain.Shared;
using UrlShortener.Infrastructure.EntityConfigurations;

namespace UrlShortener.Infrastructure.Contexts {
    public class UrlShortenerContext : DbContext, IUnitOfWork {
        
        public const string DEFAULT_SCHEMA = "core";

        public DbSet<Url> Urls { get; set; }
        public DbSet<UrlRequest> UrlRequests { get; set; }
        public DbSet<UrlDetails> UrlDetails { get; set; }

        public UrlShortenerContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new UrlEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UrlRequestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UrlDetailsEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken)) {
            // Dispatch any domain events in the same scoped transaction right before committing any aggregate changes
            //await _mediator.DispatchDomainEventsAsync(this);
            var result = await base.SaveChangesAsync();

            return true;
        }
    }
}
