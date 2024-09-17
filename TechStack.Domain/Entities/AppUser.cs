using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TechStack.Domain.Entities
{
    public class AppUser
    {
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}
