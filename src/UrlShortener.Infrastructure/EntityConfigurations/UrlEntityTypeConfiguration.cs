using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Domain.Aggregates.UrlAggregate;
using UrlShortener.Infrastructure.Contexts;

namespace UrlShortener.Infrastructure.EntityConfigurations
{
    public class UrlEntityTypeConfiguration
    : IEntityTypeConfiguration<Url>
    {
        public void Configure(EntityTypeBuilder<Url> urlConfiguration)
        {
            urlConfiguration.ToTable("Url", UrlShortenerContext.DEFAULT_SCHEMA);

            urlConfiguration.HasKey(x => x.Id);

            urlConfiguration.Ignore(x => x.DomainEvents);

            urlConfiguration.Property(x => x.Id)
                .ForSqlServerUseSequenceHiLo("UrlSeq", UrlShortenerContext.DEFAULT_SCHEMA);

            urlConfiguration.HasIndex("ShortUrl").IsUnique(true);
            urlConfiguration.HasIndex("LongUrl").IsUnique(true);
            //urlConfiguration.HasAlternateKey("ShortUrl");

            urlConfiguration.Property(x => x.ShortUrl).HasMaxLength(8).IsRequired();
            urlConfiguration.Property(x => x.LongUrl).HasMaxLength(2083).IsRequired();
            urlConfiguration.Property(x => x.CreatedOn).HasDefaultValueSql("SYSDATETIMEOFFSET()").IsRequired();

            urlConfiguration.HasOne(x => x.UrlDetails)
                .WithOne()
                .HasForeignKey<UrlDetails>(x => x.UrlId)
                .OnDelete(DeleteBehavior.Cascade);

            urlConfiguration.HasMany(x => x.UrlRequests)
               .WithOne()
               .HasForeignKey("UrlId")
               .OnDelete(DeleteBehavior.Cascade);

            urlConfiguration.Metadata.FindNavigation(nameof(Url.UrlRequests))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            //urlConfiguration.Property("UrlDetails").HasField("_urlDetails");
        }
    }
}

