using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteChecker.Models
{
    public class StatusItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public string Instance { get; set; }
        public bool Ping { get; set; }
        public int StatusCode { get; set; }
        public string Status { get; set; }
    }
}