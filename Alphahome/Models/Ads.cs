using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alphahome.Models
{
    public class Ads
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public int queue { get; set; }
    }

    public class AdsPost
    {
        public string jdoc { get; set; }
    }

    public class AdsDelete
    {
        public int id { get; set; }
    }
}
