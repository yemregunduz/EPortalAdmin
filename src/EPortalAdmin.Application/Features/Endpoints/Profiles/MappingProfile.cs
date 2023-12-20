using AutoMapper;
using EPortalAdmin.Application.ViewModels.Endpoint;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Persistence.Paging;

namespace EPortalAdmin.Application.Features.Endpoints.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Endpoint, EndpointDto>().ReverseMap();
            CreateMap<IPaginate<Endpoint>, EndpointListDto>().ReverseMap();
        }
    }
}
