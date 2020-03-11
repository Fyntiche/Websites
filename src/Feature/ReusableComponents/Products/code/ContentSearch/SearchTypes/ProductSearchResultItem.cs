using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;

namespace Websites.Feature.ReusableComponents.Products.ContentSearch.SearchTypes
{
    public class ProductSearchResultItem : SearchResultItem
    {
        [IndexField("title")]
        public virtual string Title { get; set; }

        [IndexField("description")]
        public virtual string Description { get; set; }
        
        [IndexField("short_description")]
        public virtual string ShortDescription { get; set; }

        [IndexField("category")]
        public virtual string Category { get; set; }

        [IndexField("tags_list")]
        public virtual string[] Tags { get; set; }

        [IndexField("image")]
        public virtual string Image { get; set; }

        [IndexField("price")]
        public virtual string Price { get; set; }
    }
}