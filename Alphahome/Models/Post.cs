using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alphahome.Models
{
    public class Post
    {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string content { get; set; }
        public string url { get; set; }
        public long view { get; set; }
    }

    public class PostDetail : Post
    {
        public List<ImageDetail> images { get; set; }
    }

    public class ImageDetail
    {
        public string content { get; set; }
        public string url { get; set; }
        public long id { get; set; }
    }


    public class PostParam
    {
        public string name { get; set; }
        public string description { get; set; }
        public string content { get; set; }
        public string url { get; set; }
        public string images { get; set; }
    }
    public class PostDelete
    {
        public long id { get; set; }
    }

    public class ImagePost
    {
        public string content { get; set; }
        public string url { get; set; }
        public long postId { get; set; }
    }
}
