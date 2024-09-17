using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStack.Domain.Entities.TRM;
using TechStack.Domain.Services.Interfaces.TRM;
using TechStack.Domain.Shared;

namespace TechStack.Domain.Services
{   
    public class TRMProjectService : ITRMProjectService
    {
        private readonly ITRMProjectRepository projectRepository;
        private readonly IMapper mapper;
        private readonly ILogger<TRMProjectService> logger;

        public TRMProjectService(ITRMProjectRepository projectRepository, IMapper mapper, ILogger<TRMProjectService> logger)
        {
            this.projectRepository = projectRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ExecuteResult<TRMProject>> GetProjects(string managerEmailId)
        {
            ExecuteResult<TRMProject> result = new ExecuteResult<TRMProject>();
            try
            {
                var response = await projectRepository.GetProjects(managerEmailId);
                if (response.Any())
                {
                    result.Results = response;
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
