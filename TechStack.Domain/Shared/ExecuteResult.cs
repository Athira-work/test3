using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStack.Domain.Shared
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// <see cref="ExecuteResult"/> of type <see cref="T"/> class for accessing return value.
    /// </summary>
    /// <typeparam name="T">Object.</typeparam>
    public class ExecuteResult<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecuteResult{T}"/> class.
        /// </summary>
        public ExecuteResult()
        {
            Success = false;
            Messages = new List<ExecuteMessage>();
        }

        /// <summary>
        /// Gets or sets result.
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// Gets or sets results.
        /// </summary>
        public IEnumerable<T> Results { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether success
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets messeges.
        /// </summary>
        public ICollection<ExecuteMessage> Messages { get; set; }

        /// <summary>
        /// Gets or sets the total number of records in case of paging.
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Gets or sets currentPage.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets pageSize.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets totalPages.
        /// </summary>
        public int TotalPages
        {
            get
            {
                return PageSize != 0 ? (int)Math.Ceiling((decimal)TotalRecords / PageSize) : 0;
            }
        }
    }

    /// <summary>
    /// ExecuteMessage.
    /// </summary>
    public class ExecuteMessage
    {
        /// <summary>
        /// Gets or sets code.
        /// </summary>
        public Enums.StatusCode Code { get; set; }

        /// <summary>
        /// Gets or sets description.
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// Paged list of object.
    /// </summary>
    /// <typeparam name="T"><see cref="PagedList"/> object of type <see cref="T"/></typeparam>
    public class PagedList<T>
    {
        private int _totalRecords;

        /// <summary>
        /// Gets or sets content.
        /// </summary>
        public List<T> Content { get; set; }

        /// <summary>
        /// Gets or sets currentPage.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets pageSize.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets totalRecords.
        /// </summary>
        public int TotalRecords { get => _totalRecords; set => _totalRecords = value; }

        /// <summary>
        /// Gets totalPages.
        /// </summary>
        public int TotalPages
        {
            get { return PageSize != 0 ? (int)Math.Ceiling((decimal)TotalRecords / PageSize) : 0; }
        }
    }

    /// <summary>
    /// FileResult.
    /// </summary>
    public class FileResult
    {
        /// <summary>
        /// Gets or sets fileName.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets filePath.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets fileURL.
        /// </summary>
        public string FileURL { get; set; }

        /// <summary>
        /// Gets or sets fileStream.
        /// </summary>
        public Stream FileStream { get; set; }

        /// <summary>
        /// Gets or sets fileType.
        /// </summary>
        public Enums.ContenType FileType { get; set; }

        /// <summary>
        /// Gets or sets contenType.
        /// </summary>
        public string ContenType { get; set; }
    }
}
