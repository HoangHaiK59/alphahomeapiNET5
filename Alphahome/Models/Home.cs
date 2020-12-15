using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alphahome.Models
{
    public class Home
    {
        public List<ExtractModel> services { get; set; }
        public List<ServiceType> service_types { get; set; }
    }

    public class BaseModel
    {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
    }
    public class ExtractModel: BaseModel
    {
        [JsonIgnore]
        public string service_type { get; set; }
    }
}
