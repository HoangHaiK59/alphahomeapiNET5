using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Alphahome.Models
{
    public class Service
    {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string content { get; set; }
        public string url { get; set; }
        public long view { get; set; }
        public string serviceTypeId { get; set; }
    }

    public class ManageService: Service
    {
        public string serviceTypeName { get; set; }
    }

    public class ServiceDetail: Service
    {
        public List<Image> images { get; set; }
    }

    public class ServiceHasImages : Service
    {
        public List<ImageService> images { get; set; }
    }
    public class ServicePost
    {
        public string name { get; set; }
        public string description { get; set; }
        public string content { get; set; }
        public string url { get; set; }
        public string serviceTypeId { get; set; }
        public string images { get; set; }
    }

    public class ServiceType
    {
        public string uid { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string path { get; set; }
        public string description { get; set; }
    }

    public class ServiceDelete
    {
        public long id { get; set; } 
    }
}
