namespace EPortalAdmin.Application.Pipelines.Caching
{
    public class CacheSettings
    {
        public static readonly string AppSettingsKey = "CacheSettings";
        public int SlidingExpiration { get; set; }
        public static CacheSettings Default => new() { SlidingExpiration = 1 };
    }
}
