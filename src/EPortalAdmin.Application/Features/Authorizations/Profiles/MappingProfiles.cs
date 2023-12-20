using AutoMapper;
using EPortalAdmin.Application.ViewModels.Authorization;
using EPortalAdmin.Core.Domain.Entities;

namespace EPortalAdmin.Application.Features.Authorizations.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RefreshToken, RevokedTokenDto>()
                .ReverseMap();
        }
    }
}
