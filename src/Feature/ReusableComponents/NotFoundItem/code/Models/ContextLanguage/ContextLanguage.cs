using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Globalization;

namespace Websites.Feature.ReusableComponents.NotFoundItem.Models.ContextLanguage
{
    public static class ContextLanguage
    {
        public static bool HasLanguage(this Item item, Language language)
        {
            return ItemManager.GetVersions(item, language).Count > 0;
        }

        public static bool HasLanguage(this Item item, string languageName)
        {
            return item.HasLanguage(LanguageManager.GetLanguage(languageName, item.Database));
        }

        public static bool HasContextLanguage(this Item item)
        {
            if (item == null || item.Versions == null || item.Versions.Count == 0)
                return false;
            var latestLanguageVersion = item.Versions.GetLatestVersion();
            return (latestLanguageVersion != null) && (latestLanguageVersion.Versions.Count > 0);
        }
    }
}