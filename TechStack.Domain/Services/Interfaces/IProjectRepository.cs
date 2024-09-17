using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Models.Projects.Requests;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient.Models.Users.Responses;
using TechStack.Domain.Entities;

namespace TechStack.Domain.Services.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> Collect(Action<ProjectQueryOptions> options);
        Task<IEnumerable<Project>> Collect();
        Task<Project> Collect(int projectId);
        Task<IEnumerable<GitProject>> GetProjectUsers(bool isRecentChanges = true);
    }
}
