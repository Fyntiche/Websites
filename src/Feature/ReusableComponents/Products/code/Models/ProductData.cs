using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;

namespace Websites.Feature.ReusableComponents.Products.Models
{
    public class ProductData
    {
        public List<Item> Feedback { get; set; }
        public List<Item> Tags { get; set; }
    }
}