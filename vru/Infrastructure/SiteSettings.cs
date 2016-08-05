using SX.WebCore;

namespace vru.Infrastructure
{
    public static class SiteSettings
    {
        public static SxSiteSetting Get(string key)
        {
            return MvcApplication.SiteSettingsProvider.Get(key);
        }
    }
}