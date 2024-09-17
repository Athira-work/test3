using GitLabApiClient.Models.Issues.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStack.Domain.Entities;
using TechStack.Domain.Shared;

namespace TechStack.Domain.Services.Interfaces
{
    public interface IIssueService
    {
        Task<ExecuteResult<GitIssue>> Collect(int projectId, int issueId);
        Task<ExecuteResult<GitIssue>> Collect(int projectId, Action<IssuesQueryOptions> options);
        //Task<IEnumerable<Issue>> Collect(int groupId, int? projectId = null, Action<IssuesQueryOptions> options = null);
        Task<ExecuteResult<GitIssue>> Collect(Action<IssuesQueryOptions> options);
    }
}
