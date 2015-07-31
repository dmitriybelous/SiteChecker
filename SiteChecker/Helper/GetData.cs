using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using System.Configuration;

using SiteChecker.Models;

namespace SiteChecker.Helper
{
    public static class GetData
    {
        public static List<StatusItem> GetJsonUrls()
        {
           string jsonFilePath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/" + ConfigurationManager.AppSettings["JSONFileName"]);
           List<SiteItem> items = LoadJson(jsonFilePath);

           return LoadStatusItems(items);
        }

       public static List<SiteItem> LoadJson(string path)
       {
           List<SiteItem> items = new List<SiteItem>();

           using (StreamReader r = new StreamReader(path))
           {
               string json = r.ReadToEnd();
               items = JsonConvert.DeserializeObject<List<SiteItem>>(json);
           }

           return items;
       }

       public static List<StatusItem> LoadStatusItems(List<SiteItem> items)
       {
           List<StatusItem> statusItems = new List<StatusItem>();

           foreach (var item in items)
           {
               StatusItem stem = new StatusItem();
               stem.Path = item.Path;
               stem.Name = item.Name;
               stem.Instance = item.Instance;
               stem.Type = item.Type;

               statusItems.Add(stem);
           }

           return statusItems;
       }
    }
}