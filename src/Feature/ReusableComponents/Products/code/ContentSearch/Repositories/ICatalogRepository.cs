using Websites.Feature.ReusableComponents.Products.ContentSearch.Queries;
using Websites.Feature.ReusableComponents.Products.ContentSearch.SearchTypes;
using Sitecore.ContentSearch.Linq;

namespace Websites.Feature.ReusableComponents.Products.ContentSearch.Repositories
{
    public interface ICatalogRepository
    {
        SearchResults<ProductSearchResultItem> Get(CatalogQueryArgs args);
    }
}