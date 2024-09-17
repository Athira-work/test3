using System.Threading.Tasks;
using TechStack.Domain.Entities;
using TechStack.Domain.Shared;

namespace TechStack.Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<ExecuteResult<GitUser>> GetAllAync();
        Task<ExecuteResult<GitUser>> GetAllAync(string name);
    }
}
