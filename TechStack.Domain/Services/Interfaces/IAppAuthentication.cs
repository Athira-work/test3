using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStack.Domain.Services.Interfaces
{
    public interface IAppAuthentication
    {
        string Authentication(string username, string password);
    }
}
