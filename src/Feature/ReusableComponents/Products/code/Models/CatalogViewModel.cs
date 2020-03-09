using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;

namespace Websites.Feature.ReusableComponents.Products.Models
{
    public class CatalogViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public List<FacetViewModel> Categories { get; set; }
        public List<FacetViewModel> Tags { get; set; }
        public int TotalSearchCount { get; set; }

        public int Sizepage { get; set; }
        public CatalogViewModel()
        {
            Products = new List<ProductViewModel>();
            Categories = new List<FacetViewModel>();
            Tags = new List<FacetViewModel>();

        }
    }

    public class ProductViewModel
    {
        public Item Product { get; set; }

        public IList<Item> TagsProduct { get; set; }
    }

    public class FacetViewModel
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public int Count { get; set; }
    }
}