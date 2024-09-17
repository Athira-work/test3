using GitLabApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStack.Domain.Services.Interfaces
{
    public interface IConnectionManager : IDisposable
    {
        string Version { get; }
        string PersonalAccessToken { get; }
        string GitLabApiUrl { get; }

        GitLabClient GitLabConnection();
    }
}
