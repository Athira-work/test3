using Microsoft.Extensions.Caching.Memory;
using TechStack.Domain.Shared;
using TechStack.Infrastructure.Utilities;

namespace TechStack.Services.Interfaces
{
    public class BackgroundJobs : IBackgroundJobs
    {
        private readonly IMemoryCache memoryCache;

        public BackgroundJobs(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public void RefreshProjectMemoryCache(Enums.MemoryCacheType type)
        {
            memoryCache.ClearCache(Constants.CacheGitLabProjects);
            memoryCache.ClearCacheByStartwithKey(Constants.CacheProjectMembersContains);
        }
        public void RefreshTRMResourceMemoryCache()
        {
            memoryCache.ClearCache(Constants.CacheTRMResources);
        }
    }
}
