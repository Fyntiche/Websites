using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Tasks;

namespace Websites.Project.Shop.Tasks.Commands
{
    public class SmartPublishing
    {
        public void Publish(Item[] Items, CommandItem command, ScheduleItem scheduleItem)
        {
            Sitecore.Publishing.PublishOptions publishOptions =
            new Sitecore.Publishing.PublishOptions(Database.GetDatabase("master"),
             Database.GetDatabase("web"),
             Sitecore.Publishing.PublishMode.Smart,
             Sitecore.Globalization.Language.Current,
             DateTime.Now);
            Sitecore.Publishing.Publisher publisher = new Sitecore.Publishing.Publisher(publishOptions);

            publisher.Options.Deep = true;

            publisher.Publish();
        }
    }
}