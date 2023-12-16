namespace EPortalAdmin.Core.Logging.Serilog
{
    public static class SerilogMessages
    {
        public static readonly string InvalidDefaultProvider =
            "Varsayılan olarak seçilen log provider'ı geçersiz!";
        public static readonly string NullOptionsMessage =
            "Seçili Log Provider'ı için appsettings'te ayarlar bulunamadı!";
        public static readonly string NullDefaultProvider =
            "Varsayılan loglama yöntemi appsettings'te mevcut değil, Lütfen SeriLogOptions -> DefaultProvider propertysini ekleyiniz.";
    }


}

