using System.Collections.Generic;
using System.Linq;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Fields;
using Sitecore.Diagnostics;
using Sitecore.Data.Items;

namespace Websites.Feature.ReusableComponents.Products.ContentSearch.Fields
{
    public class CategoryComputedField : IComputedIndexField
    {
        public string FieldName { get; set; }
        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            Assert.ArgumentNotNull(indexable, nameof(indexable));

            if (indexable is SitecoreIndexableItem indexableItem)
            {
                Sitecore.Data.Fields.ReferenceField referenceField  = indexableItem.Item.Fields["Category"];
                Item referencedItem = referenceField.TargetItem;

                var item = (referencedItem.Database.GetItem(referencedItem.ID, indexableItem.Item.Language));

                return item.Fields["Value"].Value;
            }

            Log.Warn($"{this} : unsupported IIndexable type : {indexable.GetType()}", this);
            return null;

        }
    }
}