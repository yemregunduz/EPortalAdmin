using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Security.JWT;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EPortalAdmin.Application.Services.AuthService
{
    public class AuthManager(IUserOperationClaimRepository userOperationClaimRepository, ITokenHelper tokenHelper,
        IRefreshTokenRepository refreshTokenRepository, IConfiguration configuration) : IAuthService
    {
        private readonly TokenOptions _tokenOptions = configuration.GetSection(TokenOptions.AppSettingsKey)
                                                        .Get<TokenOptions>()
                                                            ?? throw new NullReferenceException($"\"{TokenOptions.AppSettingsKey}\" section cannot found in configuration");

        public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
        {
            RefreshToken addedRefreshToken = await refreshTokenRepository.AddAsync(refreshToken);
            return addedRefreshToken;
        }


        public async Task<AccessToken> CreateAccessToken(User user)
        {
            IList<UserOperationClaim> userOperationClaims =
               await userOperationClaimRepository.GetAllAsync(u => u.UserId == user.Id,
                                                                include: u =>
                                                                    u.Include(u => u.OperationClaim)
               );
            IList<OperationClaim> operationClaims =
                userOperationClaims.Select(u => new OperationClaim
                { Id = u.OperationClaim.Id, Name = u.OperationClaim.Name }).ToList();

            AccessToken accessToken = tokenHelper.CreateToken(user, operationClaims);
            return accessToken;
        }

        public async Task<RefreshToken> CreateRefreshToken(User user, string ipAddress)
        {
            RefreshToken refreshToken = tokenHelper.CreateRefreshToken(user, ipAddress);
            return await Task.FromResult(refreshToken);
        }

        public async Task DeleteOldRefreshTokens(int userId)
        {
            List<RefreshToken> refreshTokens = await refreshTokenRepository
                .Query()
                .AsNoTracking()
                .Where(
                    r =>
                        r.UserId == userId
                        && r.Revoked == null
                        && r.Expires >= DateTime.UtcNow
                        && r.CreatedDate.AddDays(_tokenOptions.RefreshTokenTTL) <= DateTime.UtcNow
                )
                .ToListAsync();

            await refreshTokenRepository.DeleteRangeAsync(refreshTokens);
        }

        public async Task<RefreshToken?> GetRefreshTokenByToken(string token)
        {
            RefreshToken? refreshToken = await refreshTokenRepository.GetAsync(predicate: r => r.Token == token);
            return refreshToken;
        }

        public async Task RevokeDescendantRefreshTokens(RefreshToken refreshToken, string ipAddress, string reason)
        {
            RefreshToken? childToken = await refreshTokenRepository.GetAsync(predicate: r => r.Token == refreshToken.ReplacedByToken);
            if (childToken?.Revoked != null && childToken.Expires <= DateTime.UtcNow)
                await RevokeRefreshToken(childToken, ipAddress, reason);
            else
                await RevokeDescendantRefreshTokens(refreshToken: childToken!, ipAddress, reason);
        }

        public async Task RevokeRefreshToken(RefreshToken refreshToken, string ipAddress, string? reason = null, string? replacedByToken = null)
        {
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReasonRevoked = reason;
            refreshToken.ReplacedByToken = replacedByToken;
            await refreshTokenRepository.UpdateAsync(refreshToken);
        }

        public async Task<RefreshToken> RotateRefreshToken(User user, RefreshToken refreshToken, string ipAddress)
        {
            RefreshToken newRefreshToken = tokenHelper.CreateRefreshToken(user, ipAddress);
            await RevokeRefreshToken(refreshToken, ipAddress, reason: "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;
        }
    }
}
