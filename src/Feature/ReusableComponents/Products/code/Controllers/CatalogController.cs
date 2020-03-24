using Sitecore;
using System.Collections.Generic;
using System.Web.Mvc;
using Sitecore.Data.Items;
using Websites.Feature.ReusableComponents.Products.ContentSearch.Repositories;
using Websites.Feature.ReusableComponents.Products.ContentSearch.Queries;
using Sitecore.Mvc.Presentation;
using Websites.Feature.ReusableComponents.Products.Models;
using System.Linq;

namespace Websites.Feature.ReusableComponents.Products.Controllers
{
    public class CatalogController : Controller
    {
        
        private readonly ICatalogRepository _repository;
        private readonly CatalogModel CatalogModel;

        public CatalogController(ICatalogRepository repository)
        {
            _repository = repository;
            CatalogModel = new CatalogModel(ref _repository);
        }
       

        public ActionResult Index()
        {
            CatalogViewModel searchResult = CatalogModel.Catalog(null, null, null, null, null);
            if (searchResult.TotalSearchResults == 0)
            {
                return PartialView("~/Views/Renderings/Catalog/NoResult.cshtml", searchResult);
            }
            return PartialView("~/Views/Renderings/Catalog/Products.cshtml", searchResult);
        }


        public ActionResult ProductSearch(string query, string[] category, string page, string[] tags, string size)
        {
            CatalogViewModel searchResult = CatalogModel.Catalog(query, category, page, tags, size);
            if (searchResult.TotalSearchResults == 0)
            {
                return PartialView("~/Views/Renderings/Catalog/NoResult.cshtml", searchResult);
            }
            return PartialView("~/Views/Renderings/Catalog/ProductViews.cshtml", searchResult);
        }

        public ActionResult Paging(string query, string[] category, string page, string[] tags, string size)
        {
            CatalogViewModel searchResult = CatalogModel.Catalog(query, category, page, tags, size);
            if (searchResult.TotalSearchResults == 0)
            {
                return PartialView("~/Views/Renderings/Catalog/NoResult.cshtml", searchResult);
            }
            return PartialView("~/Views/Renderings/Catalog/PaginationResult.cshtml", searchResult);
        }

        public ActionResult Details()
        {
            List<Item> feedBackProduct = new List<Item>();
            var query = $"{Context.Item.Paths}/*[@@templatename='Feedback']";
            var feedbackItems = Context.Item.Axes.SelectItems(query);

            if (feedbackItems != null)
            {

                foreach (Item item in feedbackItems)
                {
                    feedBackProduct.Add(item);
                }
            }

            var multiListItem = Context.Item.Fields["Tags"];
            Sitecore.Data.Fields.ReferenceField referenceField = Context.Item.Fields["Category"];
            Item referencedItem = referenceField.TargetItem;

            var fieldCategory = referencedItem.Database.GetItem(referencedItem.ID);
            ProductData productData = new ProductData
            {
                Feedback = feedBackProduct,
                Tags = MultiListItems(multiListItem),
                Category = fieldCategory.Fields["Value"].Value
            };

            return View("~/Views/Renderings/Catalog/Details.cshtml", productData);
        }

        private List<Item> MultiListItems(Sitecore.Data.Fields.Field multiListItem)
        {
            Sitecore.Data.Fields.MultilistField multiListField = multiListItem;
            List<Item> items = new List<Item>();
            if (multiListField != null)
            {
                foreach (Item item in multiListField.GetItems())
                {
                    items.Add(item);
                }
            }
            return items;
        }
    }
}
