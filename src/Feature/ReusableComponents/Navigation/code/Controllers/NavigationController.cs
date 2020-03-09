using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Data.Items;
using Websites.Feature.ReusableComponents.Navigation.Models;

namespace Websites.Feature.ReusableComponents.Navigation.Controllers
{
    public class NavigationController : Controller
    {
        // GET: Navigation
        public ActionResult Index()
        {
            string HomePath = Sitecore.Context.Site.StartPath;
            var Home = Sitecore.Context.Database.GetItem(HomePath);

            MenuItemModel menuItem = new MenuItemModel(Home);

            List<MenuItemModel> menuItems = new List<MenuItemModel>();
            menuItems.Add(menuItem);

            var children = Home.GetChildren();

            foreach (Item item in children)
            {
                if (item["Show in Menu"] == "1")
                {
                    menuItems.Add(new MenuItemModel(item));
                }

            }
            MenuListModel menuList = new MenuListModel(menuItems);
            return View("~/Views/Renderings/Navigation.cshtml", menuList);
        }
    }
}