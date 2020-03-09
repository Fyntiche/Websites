using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System.Collections.Generic;
using System.Web.Mvc;
using Websites.Feature.ReusableComponents.HeroBanner.Models;

namespace Websites.Feature.ReusableComponents.HeroBanner.Controllers
{
    public class HeroBannerController : Controller
    {
        // GET: HeroBanner
        public ActionResult Index()
        {
            var TetxBackgroundCss = string.Empty;
            var ButtonBackgroundCss = string.Empty;
            var TextAlignCss = string.Empty;

            var rc = RenderingContext.CurrentOrNull;
            if (rc != null)
            {
                var parms = rc.Rendering.Parameters;

                TetxBackgroundCss = parms["Text Background Color"];
                TextAlignCss = parms["Text Align"];
                ButtonBackgroundCss = parms["Button Background Color"];
            }
            return View("~/Views/Renderings/HeroBanner.cshtml",
                new HeroBannerModel(GetDatasourceItem(), TetxBackgroundCss, TextAlignCss, ButtonBackgroundCss));
        }

        public ActionResult Slider()
        {
            List<Item> items = new List<Item>
            {
                GetDatasourceItem()
            };
            HeroBannerModel heroBannerModel = new HeroBannerModel
            {
                Items = items
            };
            return View("~/Views/Renderings/HeroBannerSlider.cshtml", heroBannerModel);
        }

        private Item GetDatasourceItem()
        {
            var datasourceID = RenderingContext.Current.Rendering.DataSource;

            return ID.IsID(datasourceID) ? Context.Database.GetItem(datasourceID) : null;
        }
    }
}