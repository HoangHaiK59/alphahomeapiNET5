using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Alphahome.Models
{
    public class AlphahomeUser
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }

    public class UserModelGet
    {
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userId { get; set; }
    }
}
