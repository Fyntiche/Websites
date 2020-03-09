using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace Websites.Feature.ReusableComponents.Navigation.Models
{
    public class MenuItemModel
    {
        public MenuItemModel(Item item)
        {
            Title = new HtmlString(FieldRenderer.Render(item, "Navigation Title"));
            IsActive = new HtmlString(FieldRenderer.Render(item, "Show in Menu"));
            Url = new HtmlString(Sitecore.Links.LinkManager.GetItemUrl(item));
        }

        public IHtmlString Title { get; set; }

        public IHtmlString IsActive { get; set; }

        public IHtmlString Url { get; set; }
    }
}