using AutoMapper;
using GitLabApiClient.Models.Projects.Requests;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStack.Domain.Entities;
using TechStack.Domain.Services.Interfaces;
using TechStack.Domain.Shared;

namespace TechStack.Domain.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository projectRepository;
        private readonly IMapper mapper;
        private readonly ILogger<ProjectService> logger;
        public ProjectService(IProjectRepository projectRepository, IMapper mapper, ILogger<ProjectService> logger)
        {
            this.projectRepository = projectRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ExecuteResult<GitProject>> Collect(Action<ProjectQueryOptions> options)
        {
            ExecuteResult<GitProject> result = new ExecuteResult<GitProject>();
            try
            {
                var response = await projectRepository.Collect(options);
                if (response != null)
                {
                    result.Results = mapper.Map<IEnumerable<GitProject>>(response);
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

        public async Task<ExecuteResult<GitProject>> Collect(string name)
        {
            ExecuteResult<GitProject> result = new ExecuteResult<GitProject>();
            try
            {
                var response = await projectRepository.Collect();
                response = response.Where(x => x.Name.ToLower().Contains(name));
                if (response != null && response.Any())
                {
                    result.Results = mapper.Map<IEnumerable<GitProject>>(response);
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

        public async Task<ExecuteResult<GitProject>> Collect(int projectId)
        {
            ExecuteResult<GitProject> result = new ExecuteResult<GitProject>();
            try
            {
                var response = await projectRepository.Collect(projectId);
                if (response != null)
                {
                    result.Result = mapper.Map<GitProject>(response);
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

        public async Task<ExecuteResult<GitProject>> CollectByUser(int userId, bool isRecentChanges = true)
        {
            ExecuteResult<GitProject> result = new ExecuteResult<GitProject>();
            try
            {
                var response = await projectRepository.GetProjectUsers(isRecentChanges);
                if (response != null && response.Any())
                {

                    result.Results = response.Where(x => x.Users.Any(y => y.Id == userId));
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
