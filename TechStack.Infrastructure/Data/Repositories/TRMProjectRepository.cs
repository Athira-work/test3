using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStack.Domain.Entities.TRM;
using TechStack.Domain.Services.Interfaces.TRM;
using TechStack.Infrastructure.Data.Dapper;

namespace TechStack.Infrastructure.Data.Repositories
{
    public class TRMProjectRepository : ITRMProjectRepository
    {
        private readonly IGenericRepository genericRepository;
        private readonly IConfiguration configuration;

        public TRMProjectRepository(IGenericRepository genericRepository, IConfiguration configuration)
        {
            this.genericRepository = genericRepository;
            this.configuration = configuration;
        }

        public async Task<IEnumerable<TRMProject>> GetProjects(string managerEmailId)
        {
            var query = @"SELECT P.Id,P.Name,P.Description,P.Type,P.ProjectManager,R.FirstName AS ProjectManagerName,
             P.CostCentre,C.Name AS CostCentreName,P.EstimatedPD,P.TotalCost,P.StartDatePlanned,P.EndDatePlanned,
	         P.StartDateActual,P.EndDateActual,P.Status,P.ProjectCode
             FROM   Project P 
             INNER JOIN Resource R ON R.Id=P.ProjectManager 
             INNER JOIN CostCentre C 
             ON C.Id=P.CostCentre AND P.Status = 1  AND R.Email2 = @ManagerEmailId
             order by P.Name";
            var dbparams = new DynamicParameters();
            dbparams.Add("ManagerEmailId", managerEmailId, DbType.String);
            var result = genericRepository.GetAll<TRMProject>(query
               , dbparams,
               commandType: CommandType.Text);
            return result;
        }
    }
}
