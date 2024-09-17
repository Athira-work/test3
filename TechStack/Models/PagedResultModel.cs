using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechStack.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// All API response will be a PagedResultModel<typeparamref name="T"/> object.
    /// </summary>
    /// <typeparam name="T">Type.</typeparam>
    public class PagedResultModel<T> : ResultModel<T>
    {
        /// <summary>
        /// Gets or sets TotalRecords.
        /// </summary>
        [JsonProperty(PropertyName = "total_records")]
        public int TotalRecords { get; set; }

        /// <summary>
        /// Gets or sets CurrentPageNo.
        /// </summary>
        [JsonProperty(PropertyName = "current_page")]
        public int CurrentPageNo { get; set; }

        /// <summary>
        /// Gets or sets PageSize.
        /// </summary>
        [JsonProperty(PropertyName = "page_size")]
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets NextPageNo.
        /// </summary>
        [JsonProperty(PropertyName = "next_page")]
        public int NextPageNo { get; set; }
    }
}
