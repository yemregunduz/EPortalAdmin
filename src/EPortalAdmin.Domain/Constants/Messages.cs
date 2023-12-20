namespace EPortalAdmin.Domain.Constants
{
    public class Messages
    {
        public static class Authorization
        {

            public static readonly string InvalidCredentials = "Giriş bilgileri hatalı!";
            public static readonly string UserNotFound = "Sisteme kayıtlı böyle bir mail adresi bulunmamaktadır.";
            public static readonly string UserCreatedSuccessfully = "Kullanıcı başarıyla oluşturuldu.";
            public static readonly string UserLoggedInSuccessfully = "Giriş başarılı!";
            public static readonly string PasswordChangedSuccessfully = "Şifreniz başarıyla değiştirildi.";
            public static readonly string SamePasswordError = "Belirlediğiniz şifre önceki şifreniz ile aynı olamaz.";
            public static readonly string InvalidPreviousPassword = "Girdiğiniz önceki şifre hatalı!";
            public static readonly string InvalidPasswordRegex = "Şifre en az 1 büyük harf, 1 küçük harf ile 1 sayı içermeli ve 8 karakterden oluşmalıdır.";
            public static readonly string EmailAlreadyExist = "Bu mail ile kayıtlı bir hesap bulunmaktadır.";
            public static readonly string ClaimsNotFound = "Kullanıcıya ait rol bulunamadı.";
            public static readonly string NotAuthorized = "Yetkisiz erişim!";
            public static readonly string UserHaveAlreadyAAuthenticator = "Kullanıcının zaten bir kimlik doğrulayıcısı var.";
            public static readonly string AlreadyVerifiedOtpAuthenticatorIsExists = "Otp kimlik doğrulayıcısının mevcut olduğu zaten doğrulandı.";
            public static readonly string RefreshTokenNotFound = "Refresh token bulunamadı.";
            public static readonly string InvalidRefreshToken = "Geçersiz refresh token!";
            public static readonly string TokenRefreshedSuccessfully = "Token başarıyla yenilendi.";
            public static readonly string RefreshTokenRevokedSuccessfully = "Refresh token başarıyla iptal edildi.";
            public static readonly string EmailAuthenticatorNotFound = "Email aktivasyonu bulunamadı.";
            public static readonly string EmailActivationKeyDoesntExists = "Email aktivasyon keyi bulunamadı.";
            public static readonly string OtpAuthenticatorNotFound = "OTP kodu bulunamadı.";
            public static readonly string OtpVerified = "OTP onaylandı.";
            public static readonly string EmailVerified = "Email onaylandı.";
        }
        public static class User
        {
            public static readonly string UserListedSuccessfully = "Kullanıcılar başarıyla listelendi.";
            public static readonly string GetUserSuccessfully = "Kullanıcı başarıyla getirildi.";
            public static readonly string UserNotFound = "Kullanıcı bulunamadı.";
        }
        public static class Endpoint
        {
            public static readonly string EndpointListedSuccessfully = "Uygulamaya ait endpointler başarıyla listelendi.";
            public static readonly string EndpointNotFound = "Endpoint bulunamadı.";
            public static readonly string GetEndpointSuccessfully = "Endpoint başarıyla getirildi.";
        }
        public static class EndpointOperationClaim
        {
            public static readonly string EndpointOperationClaimNotFound = "Kullanıcıya ait operasyon yetkisi bulunamadı";
            public static readonly string EndpointOperationClaimAlreadyExist = "Endpoint için seçtiğiniz operasyon yetkisi zaten tanımlı!";
            public static readonly string EndpointDoesNotExist = "Seçtiğiniz endpoint bulunamadı!";
            public static readonly string EndpointOperationClaimCreated = "Endpoint için seçtiğiniz operasyon yetkisi başarıyla tanımlandı.";
            public static readonly string EndpointUpdated = "Endpoint güncellendi.";
        }
        public static class OperationClaim
        {
            public static readonly string OperationClaimGetSuccessfully = "Operasyon yetkisi başarıyla getirildi.";
            public static readonly string OperationClaimsListed = "Operasyon yetkileri listelendi.";
            public static readonly string OperationClaimNotFound = "Operasyon yetkisi bulunamadı.";
            public static readonly string OperationClaimUpdated = "Operasyon yetkisi güncellendi.";
            public static readonly string OperationClaimDeleted = "Operasyon yetkisi silindi.";
            public static readonly string OperationClaimCreated = "Operasyon yetkisi başarıyla oluşturuldu.";
            public static readonly string OperationClaimAlreadyExist = "Bu operasyon yetkisi daha önce tanımlanmış!";

        }
        public static class UserOperationClaim
        {
            public static readonly string GetUserOperationClaimSuccessfully = "Kullanıcıya ait operasyon yetkisi getirildi.";
            public static readonly string OperationClaimDeletedAllUser = "İlgili operasyon yetkisi tüm kullanıcılardan silindi.";
            public static readonly string AllOperationClaimsDeletedOnUser = "Kullanıcıya ait tüm operasyon yetkileri silindi.";
            public static readonly string UserOperationClaimNotFound = "Kullanıcıya ait operasyon yetkisi bulunamadı";
            public static readonly string UserOperationClaimUpdated = "Kullanıcıya ait operasyon yetkisi güncellendi";
            public static readonly string UserOperationClaimDeleted = "Kullanıcıya ait opersayon yetkisi silindi.";
            public static readonly string UserOperationClaimCreated = "Operasyon yetkisi kullanıcıya eklendi.";
            public static readonly string UserAlreadyHasOperationClaim = "Kullanıcıda bu operasyon yetkisi zaten tanımlı!";

        }

        public static class Category
        {
            public static readonly string GetCategorySuccessfully = "Kategori getirildi.";
            public static readonly string CategoryListed = "Kategoriler listelendi";
            public static readonly string CategoryCreated = "Kategori oluşturuldu.";
            public static readonly string CategoryUpdated = "Kategori güncellendi.";
            public static readonly string CategoryDeleted = "Kategori silindi.";
            public static readonly string CategoryNotFound = "Kategori bulunamadı.";
            public static readonly string SubCategoriesDeleted = "Bu kategorinin altında bulunan tüm alt kategoriler silindi.";
        }

        public static class File
        {
            public static readonly string FileNotFound = "Dosya bulunamadı.";
            public static readonly string FileDeleted = "Dosya silindi";
            public static readonly string FilesCreated = "Dosyalar başarıyla oluşturuldu.";
            public static readonly string FileCreated = "Dosya başarıyla oluşturuldu";
            public static readonly string FilesDeleted = "Dosyalar toplu bir şekilde silindi.";
            public static readonly string FilesDeletedError = "Dosyalar toplu bir şekilde silinirken bir hata oluştu.";
            public static readonly string GetFileSuccessfully = "Dosya bilgileri başarıyla getirildi";
            public static readonly string FilesListed = "Dosyalar listelendi.";
        }

        public static class MenuItem
        {
            public static readonly string ParentMenuItemNotFound = "Seçilen üst menü geçersiz!";
            public static readonly string MenuItemNotFound = "Menü bulunamadı.";
            public static readonly string RouterLinkMustUnique = "Router link(Yönlendirme Linki) daha önce başka bir menüye atanmıştır. Yönlendirme linki benzersiz olmalıdır!";
            public static readonly string MenuItemNameMustUnique = "Menü adı daha önce alınmış, menü adı benzersiz olmalıdır!";
            public static readonly string MenuItemCreated = "Menü oluşturuldu.";
            public static readonly string MenuItemUpdated = "Menü güncellendi.";
            public static readonly string MenuItemDeleted = "Menü silindi";
            public static readonly string SubMenuItemsDeleted = "Bu menünün altında bulunan tüm alt menüler silindi";
            public static readonly string MenuItemListed = "Menü listelendi";
            public static readonly string GetMenuItemSuccessfully = "Menü elemanı getirildi.";
        }

        public static class MenuItemOperationClaim
        {
            public static readonly string MenuItemOperationClaimCreated = "Kullanıcıya menü yetkisi eklendi.";
            public static readonly string MenuItemHasOperationClaim = "Kullanıcının yetkilerinde bu menu zaten mevcut.";
            public static readonly string MenuOperationClaimNotFound = "Kullanıcı bu menüde yetkili değil.";
            public static readonly string MenuItemOperationClaimUpdated = "Kullanıcı menü yetkisi güncellendi.";
            public static readonly string MenuItemOperationClaimDeleted = "Kullanıcı menü yetkisi silindi.";
            public static readonly string MenuItemOperationClaimListed = "Kullanıcı menü yetkileri listelendi.";
            public static readonly string MenuItemOperationClaimNotFound = "Kullanıcı menü yetkisi bulunamadı.";
            public static readonly string GetMenuItemOperationClaimSuccessfully = "Kullanıcı menü yetkisi detayı getirildi.";
        }

        public static class Configuration
        {
            public static readonly string HttpContextAccessorAlreadyConfigured = "HttpContextAccessor has already been configured.";
            public static readonly string HttpContextAccessorNotConfigured = "HttpContextAccessor has not been configured.";
        }
    }
}
