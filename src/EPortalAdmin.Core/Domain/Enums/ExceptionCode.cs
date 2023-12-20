using System.ComponentModel;

namespace EPortalAdmin.Core.Domain.Enums
{
    public enum ExceptionCode
    {
        [Description("Bilinmeyen Kod")]
        Unknown,
        [Description("Email bulunamadı.")]
        EmailNotFound,
        [Description("Token bulunamadı.")]
        TokenNotFound,
        [Description("Kullanıcı bulunamadı.")]
        UserNotFound,
        [Description("Kullanıcı zaten kayıtlıdır.")]
        UserAlreadyExists,
        [Description("Kullanıcı aktif değil.")]
        UserNotActive,
        [Description("Şifre geçersizdir.")]
        InvalidPassword,
        [Description("Token geçersizdir.")]
        InvalidToken,
        [Description("Refresh token geçersizdir.")]
        InvalidRefreshToken,
        [Description("Giriş Bilgileri geçersizdir")]
        InvalidCredentials,
        [Description("Önceki şifre hatalıdır.")]
        InvalidPrevoiusPassword,
        EmailAlreadyExist,
        SamePassword,
        UserAlreadyHasAuthenticator,
        EmailActivationKeyNotFound,
        EndpoindAlreadyHasOperationClaim,
        AuthenticatorNotFound,
        AuthenticatorMustBeVerified,
        InvalidAuthenticatorCode,
        UserAlreadyHasOperationClaim,
        RefreshTokenNotFound,
        ClaimsNotFound,
        OperationClaimAlreadyExist,
        EndpointNotFound,
        InvalidDefaultProvider,
        TokenOptionsKeyNotFound,
        EndpointOperationClaimNotFound,
        OperationClaimNotFound,
        EntityNotFound,
        UserOperationClaimNotFound,
    }
}
