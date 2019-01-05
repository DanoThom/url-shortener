using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Domain.Aggregates.UrlAggregate;
using UrlShortener.WebApi.Dtos;

namespace UrlShortener.WebApi.Infrastructure.Mappings {
    public class MappingProfile : Profile {

        public MappingProfile() {
            CreateMap<Url, UrlDto>()
                .ForMember(d => d.RequestCount, c => c.MapFrom(s => s.UrlDetails == null ? 0 : s.UrlDetails.RequestCount));
        }
    }
}
