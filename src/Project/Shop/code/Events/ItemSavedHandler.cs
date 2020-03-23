using System;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Events;
using Sitecore.Links;
using Sitecore.SecurityModel;

namespace Websites.Project.Shop.Events
{
    public class ItemSavedHandler
    {
        public void OnItemSaved(object sender, EventArgs args)
        {
            // Extract the item from the event Arguments
            Item savedItem = Event.ExtractParameter(args, 0) as Item;

            // Allow only non null items and allow only items from the master database
            if (savedItem != null && savedItem.Database.Name.ToLower() == "master")
            {
                // Do some kind of template validation to limit only the items you actually want

                if (savedItem.TemplateID == ID.Parse("{C201A9D4-7402-46E7-BE6F-2273912425E0}"))
                {
                    // Get the data that you need to populate here

                    // Start Editing the Item

                    using (new SecurityDisabler())
                    {
                        savedItem.Editing.BeginEdit();
                        string messsage = $"Feedback saved { savedItem.Fields["Author"]} { savedItem.Fields["Text"]}  { savedItem.Fields["Date"]} " +
                            $"{LinkManager.GetItemUrl(savedItem)}";
                        
                        Log.Info($"{messsage}", this);


                        savedItem.Editing.EndEdit();
                    }
                }
            }
        }
    }
}