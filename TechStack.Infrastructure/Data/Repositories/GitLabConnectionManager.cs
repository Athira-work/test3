using GitLabApiClient;
using Microsoft.Extensions.Options;
using System;
using TechStack.Domain.Services.Interfaces;
using TechStack.Infrastructure.Utilities;

namespace TechStack.Infrastructure.Data.Repositories
{
    public class GitLabConnectionManager : IConnectionManager
    {
        public string Version => "v4";

        //public string PersonalAccessToken => "sawztBjy4y9r3mB3AJ4b";
        public string PersonalAccessToken => gitLabSettings.Value.PAT;
        private readonly IOptions<GitLabSettingsModel> gitLabSettings;

        //public string GitLabApiUrl => "https://git.rfldev.com/api";
        public string GitLabApiUrl => gitLabSettings.Value.APIUrl;

        private GitLabClient client;
        private bool disposedValue;

        public GitLabConnectionManager(IOptions<GitLabSettingsModel> gitLabSettings)
        {
            this.gitLabSettings = gitLabSettings;
        }

        public GitLabClient GitLabConnection()
        {
            var baseUrl = $"{ GitLabApiUrl }/{ Version }/";
            client = new GitLabClient(baseUrl, PersonalAccessToken);
            return client;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }
                client = null;
                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~GitLabConnectionManager()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
