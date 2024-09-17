using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStack.Domain.Entities;
using TechStack.Domain.Services.Interfaces.TRM;
using TechStack.Domain.Shared;

namespace TechStack.Domain.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository resourceRepository;
        private readonly IMapper mapper;
        private readonly ILogger<ResourceService> logger;
        public ResourceService(IResourceRepository resourceRepository, IMapper mapper, ILogger<ResourceService> logger)
        {
            this.resourceRepository = resourceRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ExecuteResult<Resource>> GetResources(bool includeResigned = false)
        {
            ExecuteResult<Resource> result = new ExecuteResult<Resource>();
            try
            {
                var response = await resourceRepository.GetResources(includeResigned);
                if (response.Any())
                {
                    result.Results = mapper.Map<IEnumerable<Resource>>(response);
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

        public Task<ExecuteResult<Resource>> GetResources(int id)
        {
            throw new NotImplementedException();
        }
    }
}
