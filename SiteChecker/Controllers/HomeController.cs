using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Specialized;

using SiteChecker.Models;
using SiteChecker.Helper;

namespace SiteChecker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<StatusItem> items = GetData.GetJsonUrls();

            WebSiteIsAvailable(items);
           
            return View(items);
        }

        public List<StatusItem> WebSiteIsAvailable(List<StatusItem> sites)
        {
            foreach (var item in sites)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(item.Path);
                    //set request info
                    request.Method = "HEAD";
                    request.Timeout = 5000;
                    //Set persmissions
                    request.UseDefaultCredentials = true;
                    request.PreAuthenticate = true;
                    request.Credentials = CredentialCache.DefaultCredentials;
                    // Get the response
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    // Populate properties
                    item.StatusCode = (int)response.StatusCode;
                }
                catch (WebException ex)
                {
                    //throw ex;
                }
            }

            return sites;
        }

        public bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send(nameOrAddress.Replace("http://", ""));
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            return pingable;
        }

        public double CheckInternetSpeed()
        {
            // Create Object Of WebClient
            System.Net.WebClient wc = new System.Net.WebClient();

            //DateTime Variable To Store Download Start Time.
            DateTime dt1 = DateTime.Now;

            //Number Of Bytes Downloaded Are Stored In ‘data’
            byte[] data = wc.DownloadData("http://google.com");

            //DateTime Variable To Store Download End Time.
            DateTime dt2 = DateTime.Now;

            //To Calculate Speed in Kb Divide Value Of data by 1024 And Then by End Time Subtract Start Time To Know Download Per Second.
            return Math.Round((data.Length / 1024) / (dt2 - dt1).TotalSeconds, 2);
        }
    }
}
