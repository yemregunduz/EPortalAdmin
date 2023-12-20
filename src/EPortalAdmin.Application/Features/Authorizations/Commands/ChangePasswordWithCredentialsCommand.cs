using EPortalAdmin.Application.Features.Authorizations.Rules;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Domain.Dtos;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Core.Utilities.Helpers;
using EPortalAdmin.Domain.Constants;
using MediatR;

namespace EPortalAdmin.Application.Features.Authorizations.Commands
{
    public class ChangePasswordWithCredentialsCommand : IRequest<Result>
    {
        public UserForChangePasswordDto UserForChangePasswordDto { get; set; }
        public class ChangePasswordCommandHandler(AuthorizationBusinessRules authorizationBusinessRules) 
            : ApplicationFeatureBase<User>, IRequestHandler<ChangePasswordWithCredentialsCommand, Result>
        {
            public async Task<Result> Handle(ChangePasswordWithCredentialsCommand request, CancellationToken cancellationToken)
            {
                authorizationBusinessRules.PasswordsCannotBeSameWhenPasswordChanged(request.UserForChangePasswordDto.Password, request.UserForChangePasswordDto.NewPassword);

                User? user = await Repository.GetAsync(u => u.Email == request.UserForChangePasswordDto.Email,
                    cancellationToken: cancellationToken) ??
                    throw new NotFoundException(Messages.Authorization.UserNotFound, ExceptionCode.UserNotFound);

                if (!HashingHelper.VerifyPasswordHash(request.UserForChangePasswordDto.Password, user.PasswordHash, user.PasswordSalt))
                    throw new AuthorizationException(Messages.Authorization.InvalidPreviousPassword, ExceptionCode.InvalidPrevoiusPassword);

                HashingHelper.CreatePasswordHash(request.UserForChangePasswordDto.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                User updatedUser = await Repository.UpdateAsync(user);

                return new SuccessResult(Messages.Authorization.PasswordChangedSuccessfully);
            }
        }
    }
}
