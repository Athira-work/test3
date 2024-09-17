using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechStack.Models
{
    /// <summary>
    /// All API response will be a ResultModel<typeparamref name="T"/> object.
    /// </summary>
    /// <typeparam name="T">Type.</typeparam>
    public class ResultModel<T>
    {
        /// <summary>
        /// Gets or sets Status.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets Message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets Result.
        /// </summary>
        public T Result { get; set; }
    }
}
