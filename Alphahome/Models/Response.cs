using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Alphahome.Models
{
    public class Response
    {
        public bool valid { get; set; }
        public string message { get; set; }
    }

    public class ResponseInsert
    {
        public bool valid { get; set; }
        public string message { get; set; }
    }
}
