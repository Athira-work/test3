using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStack.Domain.Shared
{
    public static class Constants
    {
        /// <summary>
        /// Set message for SuccessRequestProcessed.
        /// </summary>
        public const string SuccessRequestProcessed = "The request has been processed successfully";

        /// <summary>
        /// Set message for ErrorNoDataFound.
        /// </summary>
        public const string ErrorNoDataFound = "No data found";

        /// <summary>
        /// Set message for ErrorRequestProcessed.
        /// </summary>
        public const string ErrorRequestProcessed = "Failed to process the request.";

        /// <summary>
        /// Set message for ErrorValidation.
        /// </summary>
        public const string ErrorValidation = "Validation failed on processing the request.";
        /// <summary>
        /// Set value for APISuccessCode.
        /// </summary>
        public const int APISuccessCode = 1;

        /// <summary>
        /// Set value for APIFailureCode.
        /// </summary>
        public const int APIFailureCode = 2;

        /// <summary>
        /// Set message for APISuccess.
        /// </summary>
        public const string APISuccessText = "Success";

        /// <summary>
        /// Set message for APIFailure.
        /// </summary>
        public const string APIFailureText = "Failure";

        /// <summary>
        /// Set message for APIErrorCodesUnhandledException.
        /// </summary>
        public const string APIErrorCodesUnhandledException = "API_UnhandledException";

        /// <summary>
        /// Set message for SqlException.
        /// </summary>
        public const string SqlException = "16";

        /// <summary>
        /// Set message for SqExceptionRaisErrorNumber.
        /// </summary>
        public const int SqExceptionRaisErrorNumber = 50000;

        public const string ExcelFileExtention = ".xlsx";

        public const string DEFAULT_SHEET_NAME = "ModuleList";
        public const string DEFAULT_FILE_DATETIME = "yyyyMMdd_HHmm";
        public const string DATETIME_FORMAT = "dd/MM/yyyy hh:mm:ss";
        public const string EXCEL_MEDIA_TYPE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public const string DISPOSITION_TYPE_ATTACHMENT = "attachment";
        public const string DATE_FORMAT = "dd/MM/yyyy";

        public const string CacheProjectsByUser = "userproject_{0}";
        public const string CacheGitLabProjects = "gitlab-project";
        public const string CacheProjectMembers = "projectMembers_{0}";
        public const string CacheProjectMembersContains = "projectMembers";
        public const string CacheTRMResources = "trm_Resources";
        public const string AuthTRMUser = "trmuser";
        public const string AuthTRMPassword = "frEw3_uZ#joTe5A$OB!H";
        public const string AuthCDUser = "customerdashuser";
        public const string AuthCDPassword = "?9#ufriWoT&ohu7lSlV9";

        #region DataType available for Excel Export
        public const string STRING = "string";
        public const string INT32 = "int32";
        public const string INT64 = "int64";
        public const string DOUBLE = "double";
        public const string DATETIME = "datetime";
        public const string DECIMAL = "decimal";
        public const string BOOLEAN = "boolean";
        public const string UserStoryLabel = "User Story";
        #endregion

    }
}
