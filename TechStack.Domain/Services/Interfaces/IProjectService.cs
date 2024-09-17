using GitLabApiClient.Models.Projects.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechStack.Domain.Entities;
using TechStack.Domain.Shared;

namespace TechStack.Domain.Services.Interfaces
{
    public interface IProjectService
    {
        Task<ExecuteResult<GitProject>> Collect(Action<ProjectQueryOptions> options);
        Task<ExecuteResult<GitProject>> Collect(string name);
        Task<ExecuteResult<GitProject>> Collect(int projectId);
        Task<ExecuteResult<GitProject>> CollectByUser(int userId, bool isRecentChanges = true);
    }
}
