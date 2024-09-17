using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStack.Domain.Shared;

namespace TechStack.Services.Interfaces
{
    public interface IBackgroundJobs
    {
        void RefreshProjectMemoryCache(Enums.MemoryCacheType type);
        void RefreshTRMResourceMemoryCache();
    }
}
