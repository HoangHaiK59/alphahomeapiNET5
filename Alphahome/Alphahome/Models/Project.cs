using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alphahome.Models
{
    public class Project
    {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string content { get; set; }
        public string url { get; set; }
        public long view { get; set; }
    }
    public class ProjectPost
    {
        public string name { get; set; }
        public string description { get; set; }
        public string content { get; set; }
        public string url { get; set; }
        public string images { get; set; }
    }

    public class ProjectDetail: Project
    {
        public List<ImageProject> images { get; set; }
    }
    public class ProjectDelete
    {
        public long id { get; set; }
    }

}
