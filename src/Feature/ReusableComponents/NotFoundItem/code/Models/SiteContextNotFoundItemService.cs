using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Sites;

namespace Websites.Feature.ReusableComponents.NotFoundItem.Models
{
    //класс обслуживания, который может прочитать свойство класа Constant и загрузить его как элемент либо из идентификатора, локального или полного пути.
    public class SiteContextNotFoundItemService
    {
        protected static Item GetItemByShortPath(SiteContext siteContext, string shortPath)
        {
            shortPath = shortPath.StartsWith("/") ? shortPath.Substring(1) : shortPath;
            var fullPath = string.Concat(StringUtil.EnsurePostfix('/', siteContext.StartPath), shortPath);
            return siteContext.Database.GetItem(fullPath);
        }

        public static Item GetItemBySiteProperty(SiteContext siteContext, string propertyKey)
        {
            var property = siteContext.Properties[propertyKey];
            if (string.IsNullOrEmpty(property))
                return null;
            if (ID.IsID(property) || property.StartsWith(Sitecore.Constants.ContentPath))
                return siteContext.Database.GetItem(property);
            return GetItemByShortPath(siteContext, property);
        }

        public static bool HasNotFoundItemKey(SiteContext siteContext)
        {
            return !string.IsNullOrEmpty(siteContext.Properties[Constants.NotFoundItemPropertyKey]);
        }
    }
}