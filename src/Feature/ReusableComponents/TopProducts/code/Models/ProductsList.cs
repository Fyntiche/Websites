using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace Websites.Feature.ReusableComponents.TopProducts.Models
{
    public class ProductsList
    {
        public ProductsList(Item item)
        {
            Title = new HtmlString(FieldRenderer.Render(item, "Title"));
        }

        public IHtmlString Title { get; set; }
        public IList<Item> ProductsItem { get; set; }
    }
}