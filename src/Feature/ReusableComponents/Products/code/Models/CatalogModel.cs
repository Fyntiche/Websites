using Sitecore;
using System.Linq;
using Websites.Feature.ReusableComponents.Products.ContentSearch.Repositories;
using Websites.Feature.ReusableComponents.Products.ContentSearch.Queries;
using Sitecore.Mvc.Presentation;

namespace Websites.Feature.ReusableComponents.Products.Models
{
    public class CatalogModel
    {
        private readonly ICatalogRepository _repository;

        public CatalogModel()
        {
        }

        public CatalogModel(ICatalogRepository repository)
        {
            _repository = repository;
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

            var results = _repository.Get(args);

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