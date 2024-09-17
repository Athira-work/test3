using GitLabApiClient.Models.Users.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TechStack.Domain.Services.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> Collect();
        Task<User> Collect(string name);
    }
}
