using System.Collections.Generic;
using System.Linq;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Fields;
using Sitecore.Diagnostics;
using Sitecore.Data.Items;

namespace Websites.Feature.ReusableComponents.Products.ContentSearch.Fields
{
    public class TagsComputedField : IComputedIndexField
    {
        public string FieldName { get; set; }
        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            Assert.ArgumentNotNull(indexable, nameof(indexable));

            if (indexable is SitecoreIndexableItem indexableItem)
            {
                MultilistField field = indexableItem.Item.Fields["Tags"];
                var enItems = field.GetItems();
                List<Item> items = new List<Item>();
                foreach (var item in enItems)
                {
                    items.Add(item.Database.GetItem(item.ID, indexableItem.Item.Language));
                }
                return items.Select(x => x.Fields["Value"].Value).ToArray();
            }

            Log.Warn($"{this} : unsupported IIndexable type : {indexable.GetType()}", this);
            return null;

        }
    }
}