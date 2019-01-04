using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Domain.Aggregates.UrlAggregate;
using UrlShortener.Infrastructure.Contexts;

namespace UrlShortener.Infrastructure.EntityConfigurations
{
    public class UrlRequestEntityTypeConfiguration
    : IEntityTypeConfiguration<UrlRequest>
    {
        public void Configure(EntityTypeBuilder<UrlRequest> urlConfiguration)
        {
            urlConfiguration.ToTable("UrlRequest", UrlShortenerContext.DEFAULT_SCHEMA);

            urlConfiguration.HasKey(x => x.Id);

            urlConfiguration.Ignore(x => x.DomainEvents);

            urlConfiguration.Property(x => x.Id)
                .ForSqlServerUseSequenceHiLo("UrlRequestSeq", UrlShortenerContext.DEFAULT_SCHEMA);

            urlConfiguration.Property(x => x.AccessedOn).HasDefaultValueSql("SYSDATETIMEOFFSET()").IsRequired();
        }
    }
}

