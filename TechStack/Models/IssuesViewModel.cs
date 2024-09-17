using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechStack.Models
{
    public class IssuesViewModel
    {
        private double? plannedEffort;
        [JsonProperty(PropertyName = "module")]
        public string Module { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "plannedEffort")]
        public double? PlannedEffort {
            get
            {
                return plannedEffort;
            }
            set
            {
                plannedEffort = (value.HasValue & value > 0) ? TimeSpan.FromSeconds(value.Value).TotalHours : value;
            }
        }
        [JsonProperty(PropertyName = "plannedStartDate")]
        public string PlannedStartDate { get; set; }
        [JsonProperty(PropertyName = "plannedEnddate")]
        public string PlannedEnddate { get; set; }
        [JsonProperty(PropertyName = "plannedcost")]
        public float? Plannedcost { get; set; }
        [JsonProperty(PropertyName = "resource")]
        public string Resource { get; set; }
    }
}
