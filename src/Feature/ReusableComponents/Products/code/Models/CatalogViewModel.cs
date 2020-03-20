using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Websites.Feature.ReusableComponents.Products.ContentSearch.Queries;

namespace Websites.Feature.ReusableComponents.Products.Models
{
    public class CatalogViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public List<FacetViewModel> Categories { get; set; }
        public List<FacetViewModel> Tags { get; set; }
        public int CountPage { get; set; }
        public int TotalSearchResults { get; set; }

        public int PageFrom { get; set; }
        public int PageBefore { get; set; }

        public CatalogViewModel()
        {
            Products = new List<ProductViewModel>();
            Categories = new List<FacetViewModel>();
            Tags = new List<FacetViewModel>();

        }
    }
}