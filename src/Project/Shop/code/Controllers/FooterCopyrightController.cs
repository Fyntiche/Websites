using Sitecore;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Websites.Project.Shop.Models;

namespace Websites.Project.Shop.Controllers
{
    public class FooterCopyrightController : Controller
    {
        // GET: FooterCopyright
        public ActionResult Index()
        {
            var dataBaseId = RenderingContext.Current.Rendering.DataSource;
            FooterCopyrightModel footerCopyright = new FooterCopyrightModel();
            footerCopyright.CopyrightItem = Context.Database.GetItem(dataBaseId);
            return View("~/Views/Footer/Copyright.cshtml", footerCopyright);
        }
    }
}