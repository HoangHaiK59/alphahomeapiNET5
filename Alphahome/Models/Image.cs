using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alphahome.Models
{
    public class Image
    {
        public long id { get; set; }
        public string url { get; set; }
        public string content { get; set; }
    }

    public class ImageService : Image
    {
        public long serviceId { get; set; }
    }


    public class ImageProject: Image
    {
        public long projectId { get; set; }
    }
}
