using Sitecore;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sitecore.Data.Items;
using Websites.Feature.ReusableComponents.Products.Models;
using Sitecore.Web;
using Websites.Feature.ReusableComponents.Products.ContentSearch.Repositories;
using Websites.Feature.ReusableComponents.Products.ContentSearch.Queries;

namespace Websites.Feature.ReusableComponents.Products.Controllers
{
    public class CatalogsController : Controller
    {
        private const string Query = "query";
        private const string Tags = "tags";
        private const string Category = "category";
        private const string Page = "page";
        private const string PageSize = "size";

        private readonly ICatalogRepository _repository;

        public CatalogsController()
        {
            _repository = new CatalogRepository();
        }

        [HttpGet]
        public ActionResult Index()
        {

            var query = WebUtil.GetQueryString(Query);
            var category = WebUtil.GetQueryString(Category);
            var tags = WebUtil.GetQueryString(Tags);
            var page = WebUtil.GetQueryString(Page);
            var size = WebUtil.GetQueryString(PageSize);

            int.TryParse(page, out var pageInt);
            int.TryParse(size, out var sizeInt);
            var Size = sizeInt == 0 ? 12 : sizeInt;
            var args = new CatalogQueryArgs
            {
                Query = query,
                Category = category,
                Tags = tags.Split(new char[] { ',' }),
                Page = pageInt == 0 ? 1 : pageInt,
                Size = Size
            };
            var results = _repository.Get(args);
            var model = new CatalogViewModel
            {
                Products = results.Select(x => new ProductViewModel
                {
                    Product = Context.Database.GetItem(x.Document.ItemId)
                }).ToList()
            };
            model.TotalSearchCount = results.TotalSearchResults;
            model.Sizepage = Size;

            var categoryFacets = results.Facets.Categories.FirstOrDefault(x => x.Name == "category");
            if (categoryFacets != null)
            {
                model.Categories = categoryFacets.Values.Select(x => new FacetViewModel
                {
                    Title = x.Name,
                    Count = x.AggregateCount
                }).ToList();
            }

            var tagsFacets = results.Facets.Categories.FirstOrDefault(x => x.Name == "tags_list");
            if (tagsFacets != null)
            {
                model.Tags = tagsFacets.Values.Select(x => new FacetViewModel
                {

                    Title = x.Name,
                    Count = x.AggregateCount
                }).ToList();
            }

            foreach (var product in model.Products)
            {
                var multiListItem = product.Product.Fields["Tags"];
                product.TagsProduct = MultiListItems(multiListItem);
            }
            return View("~/Views/Renderings/Catalog/Products.cshtml", model);
        }

        public ActionResult Details()
        {
            var query = $"/sitecore/content//*[@@templatename='Home Page']/*[@@templatename='Products']//*[@@name='{Context.Item.Name}']//*[@@templatename='Feedback']";
            var feedbackItems = Context.Item.Axes.SelectItems(query);

            List<Item> feedBackItems = new List<Item>();
            foreach (Item item in feedbackItems)
            {
                feedBackItems.Add(item);
            }
   
            var multiListItem = Context.Item.Fields["Tags"];
            ProductData productData = new ProductData
            {
                Feedback = feedBackItems,
                Tags = MultiListItems(multiListItem)
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