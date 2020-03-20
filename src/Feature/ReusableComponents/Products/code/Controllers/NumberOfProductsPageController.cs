using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Data.Items;
using Sitecore;
using Websites.Feature.ReusableComponents.Products.Models;

namespace Websites.Feature.ReusableComponents.Products.Controllers
{
    public class NumberOfProductsPageController : Controller
    {
        // GET: NumberOfProductsPage
        public ActionResult Index()
        {

            var dataBaseId = RenderingContext.Current.Rendering.DataSource;
            var multiListItem = Context.Database.GetItem(dataBaseId);

            Sitecore.Data.Fields.MultilistField multiListField = multiListItem.Fields["Pages"];
            List<Item> items = new List<Item>();

            if (multiListField != null)
            {
                foreach (Item item in multiListField.GetItems())
                {
                    items.Add(item);
                }
            }
            PagesListModel pagesList = new PagesListModel(multiListItem)
            {
                PagesList = items
            };

            return View(pagesList);

        }
    }
}