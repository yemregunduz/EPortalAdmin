using AutoMapper;
using EPortalAdmin.Application.Features.EndpointOperationClaims.Commands;
using EPortalAdmin.Application.ViewModels.EndpointOperationClaim;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Persistence.Paging;

namespace EPortalAdmin.Application.Features.EndpointOperationClaims.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EndpointOperationClaim, CreateEndpointOperationClaimCommand>().ReverseMap();
            CreateMap<EndpointOperationClaim, UpdateEndpointOperationClaimCommand>().ReverseMap();
            CreateMap<EndpointOperationClaim, EndpointOperationClaimDto>().ReverseMap();
            CreateMap<IPaginate<EndpointOperationClaim>, EndpointOperationClaimListDto>().ReverseMap();
        }
    }
}
