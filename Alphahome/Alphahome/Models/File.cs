using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alphahome.Models
{
    public class File
    {
    }

    public class FileImage
    {
        public IFormFile files { get; set; }
    }
}
