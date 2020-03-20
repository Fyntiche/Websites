using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Websites.Feature.ReusableComponents.Products.ContentSearch.Queries
{
    public class CatalogQueryArgs
    {
        public CatalogQueryArgs()
        {
            Page = 1;
            Size = 12;
        }

        public string Query { get; set; }
        public string[] Categories { get; set; }
        public string[] Tags { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }

        public string Language { get; set; }
    }
}