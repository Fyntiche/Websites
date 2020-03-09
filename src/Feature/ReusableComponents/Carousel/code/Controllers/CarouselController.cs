using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Websites.ReusableComponents.Carousel.Models;

namespace Websites.ReusableComponents.Carousel.Controllers
{
    public class CarouselController : Controller
    {
        // GET: Carousel
        public ActionResult Index()
        {
            CarouselDataModel CarouselModel = new CarouselDataModel();
            Sitecore.Collections.ChildList SlideItems = null;

            //Получить текущий контекст, затем элемент источника данных
            var CurrentContext = RenderingContext.Current;
            var CurrentRendering = CurrentContext.Rendering;
            var Database = Sitecore.Context.Database;
            var DataSourceId = CurrentRendering.DataSource;

            //Создать список элементов слайдов под элементом источника данных
            if (Database.GetItem(DataSourceId) != null)
            {
                var DataSource = Database.GetItem(DataSourceId);
                SlideItems = DataSource.GetChildren();
            }

            CarouselModel.SlideItems = SlideItems;

            return PartialView(CarouselModel);
        }
    }
}