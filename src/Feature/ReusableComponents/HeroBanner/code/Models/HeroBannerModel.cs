using System.Collections.Generic;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Links;

namespace Websites.Feature.ReusableComponents.HeroBanner.Models
{
    public class HeroBannerModel
    {
        public HeroBannerModel(Item item, string TextColor, string Align, string ButtonColor)
        {
            Title = new HtmlString(Sitecore.Web.UI.WebControls.FieldRenderer.Render(item, "Title"));
            var LinkClass = $"class = {ButtonColor} btn-lg";
            Description = new HtmlString(Sitecore.Web.UI.WebControls.FieldRenderer.Render(item, "Description"));
            Link = new HtmlString(Sitecore.Web.UI.WebControls.FieldRenderer.Render(item, "Call To Action Url", LinkClass));
            BackgroundColor = TextColor;
            TextAlign = Align;
        }

        public HeroBannerModel()
        {         
        }

        public IHtmlString Title { get; set; }
        public IHtmlString Description { get; set; }
        public IHtmlString Link { get; set; }
        public List<Item> Items { get; set;  }

        public string BackgroundColor { get; set; }
        public string TextAlign { get; set; }
    }
}