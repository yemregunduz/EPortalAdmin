namespace EPortalAdmin.Application.Pipelines.Caching
{
    public class CacheSettings
    {
        public static readonly string AppSettingsKey = "CacheSettings";
        public int SlidingExpiration { get; set; }
    }
}
