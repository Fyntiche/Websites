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
        
        private readonly ICatalogRepository repository;

        public CatalogController(ICatalogRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            CatalogViewModel searchResult = Catalog(null, null, null, null, null);
            if (searchResult.TotalSearchResults == 0)
            {
                return PartialView("~/Views/Renderings/Catalog/NoResult.cshtml", searchResult);
            }
            return PartialView("~/Views/Renderings/Catalog/Products.cshtml", searchResult);
        }


        public ActionResult ProductSearch(string query, string[] category, string page, string[] tags, string size)
        {
            CatalogViewModel searchResult = Catalog(query, category, page, tags, size);
            if (searchResult.TotalSearchResults == 0)
            {
                return PartialView("~/Views/Renderings/Catalog/NoResult.cshtml", searchResult);
            }
            return PartialView("~/Views/Renderings/Catalog/ProductViews.cshtml", searchResult);
        }

        public ActionResult Paging(string query, string[] category, string page, string[] tags, string size)
        {
            CatalogViewModel searchResult = Catalog(query, category, page, tags, size);
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

        public CatalogViewModel Catalog(string query, string[] categories, string page, string[] tags, string size)
        {
            int.TryParse(page, out var pageInt);
            int.TryParse(size, out var sizeInt);

            var rc = RenderingContext.CurrentOrNull;
            string countProduct = null;
            if (rc != null)
            {
                var parms = rc.Rendering.Parameters;

                countProduct = parms["Count Product On Page"];
            }
            int.TryParse(countProduct, out var countProductOnPage);
            countProductOnPage = countProductOnPage == 0 ? 12 : countProductOnPage;
            var args = new CatalogQueryArgs
            {
                Query = query,
                Page = pageInt == 0 ? 1 : pageInt,
                Size = sizeInt == 0 ? countProductOnPage : sizeInt,
                Language = Context.Language.CultureInfo.Name,
                Tags = tags,
                Categories = categories
            };

            var results = repository.Get(args);

            var model = new CatalogViewModel
            {
                Products = results.Select(x => new ProductViewModel
                {
                    Categories = x.Document.Category,
                    Description = x.Document.Description,
                    Price = x.Document.Price,
                    ShortDescription = x.Document.ShortDescription,
                    Title = x.Document.Title,
                    Image = x.Document.Image,
                    TagsProduct = x.Document.Tags,
                    Url = x.Document.Url
                }).ToList()
            };

            model.CountPage = (results.TotalSearchResults / args.Size) + 1;
            model.TotalSearchResults = results.TotalSearchResults;
            model.PageFrom = args.Size * (args.Page - 1) + 1;
            model.PageBefore = args.Size * args.Page;
            if (model.PageBefore > results.TotalSearchResults)
            {
                model.PageBefore = results.TotalSearchResults;
            }

            var categoryFacets = results.Facets.Categories.FirstOrDefault(x => x.Name == "product_category");
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

            return model;

        }
    }
}
