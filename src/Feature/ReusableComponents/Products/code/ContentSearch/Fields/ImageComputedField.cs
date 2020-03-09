using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Fields;
using Sitecore.Diagnostics;
using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Websites.Feature.ReusableComponents.Products.ContentSearch.Fields
{
    public class ImageComputedField : IComputedIndexField
    {
        public string Parameters { get; set; }
        public string FieldName { get; set; }
        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            Assert.ArgumentNotNull(indexable, "indexable");

            if (!(indexable is SitecoreIndexableItem indexableItem))
            {
                Log.Warn(string.Format("{0} : unsupported IIndexable type : {1}", this, indexable.GetType()), this);
                return null;
            }

            ImageField img = indexableItem.Item.Fields["Image"];

            return (img == null || img.MediaItem == null) ? null : MediaManager.GetMediaUrl(img.MediaItem);
        }
    }
}