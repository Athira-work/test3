using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStack.Domain.Shared
{
    public class Enums
    {
        /// <summary>
        /// Content Type.
        /// </summary>
        public enum ContenType
        {
            /// <summary>
            /// PDF
            /// </summary>
            [Description("PDF")]
            PDF,

            /// <summary>
            /// JPG
            /// </summary>
            [Description("JPEG")]
            JPEG,
        }

        /// <summary>
        /// StatusCode.
        /// </summary>
        public enum StatusCode
        {
            /// <summary>
            /// Denote the success status.
            /// </summary>
            [Description("Success")]
            Success,

            /// <summary>
            /// Denote error status.
            /// </summary>
            [Description("Error")]
            Error,

            /// <summary>
            /// NO record found.
            /// </summary>
            [Description("No Record Found")]
            NoRecordFound,
        }

        public enum MemoryCacheType
        {
            All = 1,
            GitLabProjects = 2,
            GitLabUsers = 3,
            GitLabProjectByUsers = 4

        }
    }
}
