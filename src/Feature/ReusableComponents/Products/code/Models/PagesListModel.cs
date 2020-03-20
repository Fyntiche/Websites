using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace Websites.Feature.ReusableComponents.Products.Models
{
    public class PagesListModel
    {
        public PagesListModel(Item item)
        {
            Title = new HtmlString(FieldRenderer.Render(item, "Title"));
        }
        public IList<Item> PagesList { get; set; }

        public IHtmlString Title { get; set; }
    }
}