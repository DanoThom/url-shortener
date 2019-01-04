using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Domain.Aggregates.UrlAggregate;
using UrlShortener.Infrastructure.Contexts;

namespace UrlShortener.Infrastructure.EntityConfigurations
{
    public class UrlDetailsEntityTypeConfiguration
    : IEntityTypeConfiguration<UrlDetails>
    {
        public void Configure(EntityTypeBuilder<UrlDetails> urlConfiguration)
        {
            urlConfiguration.ToTable("UrlDetails", UrlShortenerContext.DEFAULT_SCHEMA);

            urlConfiguration.HasKey(x => x.Id);

            urlConfiguration.Ignore(x => x.DomainEvents);

            urlConfiguration.Property(x => x.Id)
               .ForSqlServerUseSequenceHiLo("UrlDetailsSeq", UrlShortenerContext.DEFAULT_SCHEMA);

            urlConfiguration.Property(x => x.RequestCount);

            urlConfiguration.HasOne<Url>("Url")
                .WithOne(x => x.UrlDetails)
                .HasForeignKey<Url>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

