using System.Linq;
using Websites.Feature.ReusableComponents.Products.ContentSearch.Queries;
using Websites.Feature.ReusableComponents.Products.ContentSearch.SearchTypes;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.ContentSearch.SearchTypes;

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
                searchPredicate = searchPredicate.And(x => x.Title.Contains(args.Query) 
                    || x.Tags.Contains(args.Query) 
                    || x.Category.Contains(args.Query)
                    || x.ShortDescription.Contains(args.Query)
                    || x.Description.Contains(args.Query)
                    );
            }



            if (args.Categories!= null)
            {
                foreach (var category in args.Categories)
                {
                    searchPredicate = searchPredicate.Or(x => x.Category.Equals(category));
                }
            }

            if (args.Tags!=null)
            {
                foreach (var tag in args.Tags)
                {
                    searchPredicate = searchPredicate.And(x => x.Tags.Contains(tag));
                }
            }
            


            if (!string.IsNullOrEmpty(args.Language))
            {
                searchPredicate = searchPredicate.And(x => x.Language.Equals(args.Language));
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