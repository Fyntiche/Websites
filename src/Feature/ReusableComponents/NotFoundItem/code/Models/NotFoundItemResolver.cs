using Sitecore;
using Sitecore.Pipelines.HttpRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Web;
using System.IO;
using Websites.Feature.ReusableComponents.NotFoundItem.Models.ContextLanguage;

namespace Websites.Feature.ReusableComponents.NotFoundItem.Models
{
    public class NotFoundItemResolver : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            if (IsValidContextItemResolved()
              || !SiteContextNotFoundItemService.HasNotFoundItemKey(Context.Site)
              || args.LocalPath.StartsWith("/sitecore")
              || RequestIsForPhysicalFile(args.Url.FilePath))
                return;

            Context.Item = GetSiteSpecificNotFoundItem();

            if (Context.Item == null)
                return;
            ItemNotFoundStatusRepository.Set(true);
        }

        protected virtual bool IsValidContextItemResolved()
        {
            if (Context.Item == null || !Context.Item.HasContextLanguage())
                return false;
            return !(Context.Item.Visualization.Layout == null
              && string.IsNullOrEmpty(WebUtil.GetQueryString("sc_layout")));
        }

        protected virtual bool RequestIsForPhysicalFile(string filePath)
        {
            return File.Exists(HttpContext.Current.Server.MapPath(filePath));
        }

        protected virtual Item GetSiteSpecificNotFoundItem()
        {
            return SiteContextNotFoundItemService.GetItemBySiteProperty(Context.Site, Constants.NotFoundItemPropertyKey);
        }
    }
}