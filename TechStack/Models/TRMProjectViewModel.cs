using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechStack.Models
{
    public class TRMProjectViewModel
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string Description { get; set; }

        public int ProjectManagerId { get; set; }

        public string ProjectManagerName { get; set; }
        public string CostCentreName { get; set; }

        public float EstimatedPD { get; set; }
        public DateTime StartDatePlanned { get; set; }

        public DateTime EndDatePlanned { get; set; }

        public DateTime EndDateActual { get; set; }

        public DateTime StartDateActual { get; set; }
        public string ProjectCode { get; set; }
    }
}
