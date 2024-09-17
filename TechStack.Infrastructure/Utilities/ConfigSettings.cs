using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStack.Infrastructure.Utilities
{
    public class GitLabSettingsModel
    {
        public string PAT { get; set; }
        public string IncludeLabels { get; set; }
        public string LastActivityType { get; set; }
        public int LastActivityBefore { get; set; }
        public string APIUrl { get; set; }
    }

}
