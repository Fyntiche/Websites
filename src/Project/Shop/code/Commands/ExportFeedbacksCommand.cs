using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Shell.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jitbit.Utils;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Web.UI.Sheer;
using System.IO;
using Sitecore.IO;

namespace Websites.Project.Shop.Commands
{
    public class ExportFeedbacksCommand : Command
    {
        private const string ProductTemplateID = "{959CDED3-F9F5-478B-A655-43D4C8B1CD73}";
        public override CommandState QueryState(CommandContext context)
        {
            if (context.Items.Length > 0 && context.Items[0].TemplateID.Equals(new ID(ProductTemplateID)))
            {
                return base.QueryState(context);
            }
            return CommandState.Hidden;
        }
        public override void Execute(CommandContext context)
        {
            var item = context.Items.FirstOrDefault();
            IEnumerable<Item> ChildrenItem = item.Children;
            CsvExport ChildExport = new CsvExport();
            foreach (Item child in ChildrenItem)
            {
                ChildExport.AddRow();
                ChildExport["Author"] = child.Fields["Author"].Value;
                ChildExport["Text"] = child.Fields["Text"].Value;
                ChildExport["Date"] = DateUtil.IsoDateToDateTime(child.Fields["Date"].Value);
            }
            string tempfolder = MainUtil.MapPath(Settings.TempFolderPath);
            string pathToFile = $@"{tempfolder}\{ItemUtil.ProposeValidItemName(item.Name)}ChildExport.csv";
            ChildExport.ExportToFile(pathToFile);

            Context.ClientPage.ClientResponse.Download(pathToFile);
        }
    }
}