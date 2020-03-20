using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;

namespace Websites.Feature.ReusableComponents.Products.Models
{
    public class ProductViewModel
    {
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public string Categories { get; set; }

        public string Image { get; set; }

        public string[] TagsProduct { get; set; }

        public string Url { get; set; }
    }
}