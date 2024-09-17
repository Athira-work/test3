using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechStack.Domain.Entities;
using TechStack.Domain.Services.Interfaces;
using TechStack.Domain.Shared;

namespace TechStack.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly ILogger<UserService> logger;

        public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ExecuteResult<GitUser>> GetAllAync()
        {
            ExecuteResult<GitUser> result = new ExecuteResult<GitUser>();
            try
            {
                var response = await userRepository.Collect();
                if (response != null)
                {
                    result.Results = mapper.Map<IEnumerable<GitUser>>(response);
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

        public async Task<ExecuteResult<GitUser>> GetAllAync(string name)
        {
            ExecuteResult<GitUser> result = new ExecuteResult<GitUser>();
            try
            {
                var response = await userRepository.Collect(name);
                if (response != null)
                {
                    result.Result = mapper.Map<GitUser>(response);
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
