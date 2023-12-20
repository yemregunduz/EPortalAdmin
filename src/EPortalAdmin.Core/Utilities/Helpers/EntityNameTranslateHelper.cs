namespace EPortalAdmin.Core.Utilities.Helpers
{
    public class EntityNameTranslateHelper
    {
        private static Dictionary<string, string> TurkishEntityNames = new()
        {
            {"OperationClaim" , "Operasyon Yekisi" },
            {"UserOperationClaim" , "Kullanıcıya Operasyon Yetkisi" },
            {"User" , "Kullanıcı" }
        };
        public static string Turkish(string key)
        {
            string? value = TurkishEntityNames.GetValueOrDefault(key);
            if (value is null)
                return key;

            return value;
        }
    }
}
