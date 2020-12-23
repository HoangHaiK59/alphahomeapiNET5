using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alphahome.Models
{
    public class Search
    {
        public long id { get; set; }
        public string name { get; set; }
        public string service_type { get; set; }
        public long postId { get; set; }
    }
}
