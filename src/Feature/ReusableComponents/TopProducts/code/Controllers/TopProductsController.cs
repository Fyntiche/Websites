using Sitecore;
using Sitecore.Data;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Data.Items;
using Websites.Feature.ReusableComponents.TopProducts.Models;

namespace Websites.Feature.ReusableComponents.TopProducts.Controllers
{
    public class TopProductsController : Controller
    {
        // GET: TopProducts
        public ActionResult Index()
        {
            

            var dataBaseId = RenderingContext.Current.Rendering.DataSource;
            var multiListItem = Context.Database.GetItem(dataBaseId);

            Sitecore.Data.Fields.MultilistField multiListField = multiListItem.Fields["ProductList"];
            List<Item> items = new List<Item>();
                
            if (multiListField != null)
            {
                foreach (Item item in multiListField.GetItems())
                {
                    items.Add(item);
                }
            }
            ProductsList productsList = new ProductsList(multiListItem);
            productsList.ProductsItem = items;
            return View(productsList);
        }
    }
}