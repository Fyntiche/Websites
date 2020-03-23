using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Websites.Feature.ReusableComponents.NotFoundItem.Models
{
    public class ItemNotFoundStatusRepository
    {
        public static bool Get()
        {
            return HttpContext.Current.Items[Constants.NotFoundItemPropertyKey] != null && (bool)HttpContext.Current.Items[Constants.NotFoundItemPropertyKey];
        }

        public static void Set(bool status)
        {
            HttpContext.Current.Items[Constants.NotFoundItemPropertyKey] = status;
        }
    }
}