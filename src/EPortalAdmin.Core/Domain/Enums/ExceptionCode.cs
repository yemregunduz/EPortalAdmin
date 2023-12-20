using System.ComponentModel;

namespace EPortalAdmin.Core.Domain.Enums
{
    public enum ExceptionCode
    {
        [Description("Bilinmeyen Kod")]
        Unknown = 999,
        #region NotFoundExceptionCodes (1000-1999)

        [Description("E-posta bulunamadı.")]
        EmailNotFound = 1000,

        [Description("Token bulunamadı.")]
        TokenNotFound = 1001,

        [Description("Refresh Token Bulunamadı.")]
        RefreshTokenNotFound = 1002,

        [Description("Kullanıcı bulunamadı.")]
        UserNotFound = 1003,

        [Description("Yetkiler Bulunamadı.")]
        ClaimsNotFound = 1004,

        [Description("Endpoint Bulunamadı.")]
        EndpointNotFound = 1005,

        [Description("TokenOptions Anahtarı Appsettings içerisinde bulunamadı.")]
        TokenOptionsKeyNotFound = 1006,

        [Description("Endpoint İşlem Yetkisi Bulunamadı.")]
        EndpointOperationClaimNotFound = 1007,

        [Description("İşlem Yetkisi Bulunamadı.")]
        OperationClaimNotFound = 1008,

        [Description("Veritabanı Nesnesi Bulunamadı.")]
        EntityNotFound = 1009,

        [Description("Kullanıcı İşlem Yetkisi Bulunamadı.")]
        UserOperationClaimNotFound = 1010,

        [Description("E-posta Aktivasyon Anahtarı Bulunamadı.")]
        EmailActivationKeyNotFound = 1011,

        [Description("Doğrulayıcı Bulunamadı.")]
        AuthenticatorNotFound = 1012,

        #endregion

        #region BusinessExceptionCodes (2000-2999)

        [Description("Geçersiz Varsayılan Sağlayıcı.")]
        InvalidDefaultProvider = 2000,

        [Description("Geçersiz Doğrulayıcı Kodu.")]
        InvalidAuthenticatorCode = 2001,

        [Description("Kullanıcı Zaten İşlem Yetkisine Sahip.")]
        UserAlreadyHasOperationClaim = 2002,

        [Description("Geçersiz Refresh Token.")]
        InvalidRefreshToken = 2003,

        [Description("Endpoint Zaten İşlem Yetkisi İçeriyor.")]
        EndpointAlreadyHasOperationClaim = 2004,

        [Description("Kullanıcı Zaten Doğrulayıcıya Sahip.")]
        UserAlreadyHasAuthenticator = 2005,

        [Description("Önceki Şifre ile Aynı Şifre.")]
        SamePassword = 2006,

        [Description("Bu e-Posta ile sisteme kayıtlı kullanıcı bulunmaktadır.")]
        EmailAlreadyExist = 2007,

        [Description("Kullanıcı aktif değil.")]
        UserNotActive = 2008,

        [Description("İşlem Yetkisi Zaten Mevcut.")]
        OperationClaimAlreadyExist = 2009,

        [Description("Doğrulayıcı Doğrulanmalı.")]
        AuthenticatorMustBeVerified = 2010,

        [Description("Kullanıcı zaten kayıtlı.")]
        UserAlreadyExists = 2011,
        #endregion

        #region AuthorizationExceptionCodes (3000-3999)
        [Description("Giriş bilgileri geçersiz!")]
        InvalidCredentials = 3000,

        [Description("Önceki şifreniz hatalı!")]
        InvalidPreviousPassword = 3001,

        [Description("Geçersiz şifre!")]
        InvalidPassword = 3002,

        [Description("Geçersiz token!")]
        InvalidToken = 3003

        #endregion





















    }

}
