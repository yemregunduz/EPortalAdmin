using AutoMapper;
using EPortalAdmin.Application.Features.UserOperationClaims.Commands;
using EPortalAdmin.Application.ViewModels.UserOperationClaim;
using EPortalAdmin.Core.Domain.Entities;

namespace EPortalAdmin.Application.Features.UserOperationClaims.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateUserOperationClaimCommand, UserOperationClaim>().ReverseMap();
            CreateMap<UpdateUserOperationClaimCommand, UserOperationClaim>().ReverseMap();
            CreateMap<UserOperationClaimDto, UserOperationClaim>()
                .ForMember(uoc => uoc.OperationClaim, opt => opt.MapFrom(uoc => uoc.OperationClaimDto))
                .ReverseMap();
        }
    }
}
