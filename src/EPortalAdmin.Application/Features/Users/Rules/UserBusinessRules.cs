using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Domain.Constants;

namespace EPortalAdmin.Application.Features.Users.Rules
{
    public class UserBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public UserBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CheckIfUserExist(int userId)
        {
            User? user = await _userRepository.GetAsync(u => u.Id == userId)
                ?? throw new BusinessException(Messages.User.UserNotFound, ExceptionCode.UserNotFound);
        }
    }
}
