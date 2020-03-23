using Sitecore.Pipelines.HttpRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Websites.Feature.ReusableComponents.NotFoundItem.Models
{
    public class SetNotFoundStatusCode : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            if (!ItemNotFoundStatusRepository.Get())
                return;
            HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.NotFound;
            HttpContext.Current.Response.TrySkipIisCustomErrors = true;
        }
    }
}