using AutoMapper;
using EPortalAdmin.Application.ViewModels.User;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Persistence.Paging;

namespace EPortalAdmin.Application.Features.Users.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<IPaginate<User>, UserListDto>().ReverseMap();
        }
    }
}
