using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStack.API;
using TechStack.Domain.Shared;
using TechStack.Services.Interfaces;

namespace TechStack.Extensions
{
    public static class ConfigureRecurringJobs
    {
        public static void ConfigureJobs(IServiceProvider serviceProvider, IRecurringJobManager recurringJobManager)
        {
            string projectMemoryCacheCrons = Startup.StaticConfig.GetSection("JobCrons").GetSection("RefreshProjectMemoryCache").GetSection("Crons").Value;
            string trmResourceMemoryCache = Startup.StaticConfig.GetSection("JobCrons").GetSection("RefreshTRMResourceMemoryCache").GetSection("Crons").Value;
            recurringJobManager.AddOrUpdate(
                            Startup.StaticConfig.GetSection("JobCrons").GetSection("RefreshProjectMemoryCache").GetSection("Name").Value,
                            () => serviceProvider.GetService<IBackgroundJobs>().RefreshProjectMemoryCache(Enums.MemoryCacheType.All),
                            projectMemoryCacheCrons
                            );

            recurringJobManager.AddOrUpdate(
                        Startup.StaticConfig.GetSection("JobCrons").GetSection("RefreshTRMResourceMemoryCache").GetSection("Name").Value,
                          () => serviceProvider.GetService<IBackgroundJobs>().RefreshTRMResourceMemoryCache(),
                          trmResourceMemoryCache
                          );
        }
    }
}
