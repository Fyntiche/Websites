using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Websites.Feature.ReusableComponents.Navigation.Models
{
    public class MenuListModel
    {
        public IList<MenuItemModel> MenuList { get; set; }

        public MenuListModel(IList<MenuItemModel> menuList)
        {
            MenuList = menuList;
        }
    }
}