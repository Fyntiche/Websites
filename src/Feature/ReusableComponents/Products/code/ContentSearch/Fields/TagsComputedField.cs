using System.Linq;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Fields;
using Sitecore.Diagnostics;

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
                var items = field.GetItems();
                return items.Select(x => x.Fields["Value"].Value);
            }

            Log.Warn($"{this} : unsupported IIndexable type : {indexable.GetType()}", this);
            return null;

        }
    }
}