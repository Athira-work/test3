using AutoMapper;
using GitLabApiClient.Models.Issues.Requests;
using GitLabApiClient.Models.Issues.Responses;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStack.Domain.Entities;
using TechStack.Domain.Services.Interfaces;
using TechStack.Domain.Services.Interfaces.TRM;
using TechStack.Domain.Shared;

namespace TechStack.Domain.Services
{
    public class IssueService : IIssueService
    {
        private readonly IIssueRepository issueRepository;
        private readonly IMapper mapper;
        private readonly ILogger<IssueService> logger;

        public IssueService(IIssueRepository issueRepository, IMapper mapper, ILogger<IssueService> logger)
        {
            this.issueRepository = issueRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ExecuteResult<GitIssue>> Collect(int projectId, int issueId)
        {
            ExecuteResult<GitIssue> result = new ExecuteResult<GitIssue>();
            try
            {
                var response = await issueRepository.Collect(projectId, issueId);
                if (response != null)
                {
                    result.Results = mapper.Map<IEnumerable<GitIssue>>(response);
                    result.Success = true;
                    result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() { Code = Enums.StatusCode.Success, Description = Constants.SuccessRequestProcessed },
                    };
                }
                else
                {
                    result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() { Code = Enums.StatusCode.NoRecordFound, Description = Constants.ErrorNoDataFound },
                    };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, Constants.ErrorRequestProcessed);
            }
            return result;
        }

        public async Task<ExecuteResult<GitIssue>> Collect(int projectId, Action<IssuesQueryOptions> options)
        {
            ExecuteResult<GitIssue> result = new ExecuteResult<GitIssue>();
            try
            {
                var response = await issueRepository.Collect(projectId, options);
                if (response != null)
                {
                    result.Results = mapper.Map<IEnumerable<GitIssue>>(response);
                    result.Success = true;
                    result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() { Code = Enums.StatusCode.Success, Description = Constants.SuccessRequestProcessed },
                    };
                }
                else
                {
                    result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() { Code = Enums.StatusCode.NoRecordFound, Description = Constants.ErrorNoDataFound },
                    };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, Constants.ErrorRequestProcessed);
            }
            return result;
        }

        public async Task<ExecuteResult<GitIssue>> Collect(Action<IssuesQueryOptions> options)
        {
            ExecuteResult<GitIssue> result = new ExecuteResult<GitIssue>();
            try
            {
                var response = await issueRepository.Collect(options);
                if (response != null)
                {
                    result.Results = mapper.Map<IEnumerable<GitIssue>>(response);
                    result.Success = true;
                    result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() { Code = Enums.StatusCode.Success, Description = Constants.SuccessRequestProcessed },
                    };
                }
                else
                {
                    result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() { Code = Enums.StatusCode.NoRecordFound, Description = Constants.ErrorNoDataFound },
                    };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, Constants.ErrorRequestProcessed);
            }
            return result;
        }

        public async Task<ExecuteResult<GitIssue>> Collect(int projectId, DateTime fromDate, DateTime toDate)
        {
            ExecuteResult<GitIssue> result = new ExecuteResult<GitIssue>();
            try
            {
                IEnumerable<Issue> response = Enumerable.Empty<Issue>();
                await issueRepository.Collect(projectId, (options) =>
                {
                    options.State = GitLabApiClient.Models.Issues.Responses.IssueState.All;
                    options.CreatedAfter = fromDate;
                    options.CreatedBefore = toDate.AddDays(1);  // current date
                });
                if (response != null)
                {
                    result.Results = mapper.Map<IEnumerable<GitIssue>>(response);
                    result.Success = true;
                    result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() { Code = Enums.StatusCode.Success, Description = Constants.SuccessRequestProcessed },
                    };
                }
                else
                {
                    result.Messages = new List<ExecuteMessage>()
                    {
                        new ExecuteMessage() { Code = Enums.StatusCode.NoRecordFound, Description = Constants.ErrorNoDataFound },
                    };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, Constants.ErrorRequestProcessed);
            }
            return result;
        }
    }
}
