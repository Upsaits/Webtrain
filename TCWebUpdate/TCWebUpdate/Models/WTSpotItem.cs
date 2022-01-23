using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCWebUpdate.Models
{
    public class WTSpotItem
    {
        public string Id { get; set; }
        public string WTSpotName { get; set; }
        public string Description { get; set; }
        public string LocationName { get; set; }
        public string IPAddress { get; set; }
        public string SSIDName { get; set; }
        public string SSIDPassword { get; set; }
    }
}