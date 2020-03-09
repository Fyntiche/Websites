using System.Linq;
using Websites.Feature.ReusableComponents.Products.ContentSearch.Queries;
using Websites.Feature.ReusableComponents.Products.ContentSearch.SearchTypes;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;

namespace Websites.Feature.ReusableComponents.Products.ContentSearch.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly string IndexName = $"custom_{Sitecore.Context.Database.Name.ToLower()}_index";

        private ISearchIndex _index;

        private ISearchIndex Index => _index ?? (_index = ContentSearchManager.GetIndex(IndexName));

        private IProviderSearchContext _context;

        private IProviderSearchContext Context => _context ?? (_context = Index.CreateSearchContext());

        public SearchResults<ProductSearchResultItem> Get(CatalogQueryArgs args)
        {
            var searchPredicate = PredicateBuilder.True<ProductSearchResultItem>();

            if (!string.IsNullOrEmpty(args.Query))
            {
                searchPredicate = searchPredicate.And(x => x.Title.Contains(args.Query));
            }

            if (!string.IsNullOrEmpty(args.Category))
            {
                searchPredicate = searchPredicate.And(x => x.Category.Equals(args.Category));
            }

            if (!string.IsNullOrEmpty(args.Tag))
            {
                searchPredicate = searchPredicate.And(x => x.Tags.Contains(args.Tag));
            }

            var result = Context.GetQueryable<ProductSearchResultItem>()
                .Where(searchPredicate)
                .FacetOn(x => x.Category, 1)
                .FacetOn(x => x.Tags, 1)
                .Page(args.Page - 1, args.Size)
                .GetResults();

            return result;
        }
    }
}