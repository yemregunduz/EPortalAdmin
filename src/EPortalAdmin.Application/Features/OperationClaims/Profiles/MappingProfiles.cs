using AutoMapper;
using EPortalAdmin.Application.Features.OperationClaims.Commands;
using EPortalAdmin.Application.ViewModels.OperationClaim;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Persistence.Paging;

namespace EPortalAdmin.Application.Features.OperationClaims.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<OperationClaim, OperationClaimDto>().ReverseMap();
            CreateMap<OperationClaim, CreateOperationClaimCommand>().ReverseMap();
            CreateMap<OperationClaim, UpdateOperationClaimCommand>().ReverseMap();
            CreateMap<IPaginate<OperationClaim>, OperationClaimListDto>().ReverseMap();
        }
    }
}
