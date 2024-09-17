using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStack.Domain.Entities;

namespace TechStack.Models
{
    public class AuthenticateResponse
    {
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(string userName, string token)
        {
            Username = userName;
            Token = token;
        }
    }
}
