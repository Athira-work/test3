using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using TechStack.Domain.Shared;
using TechStack.Models;

namespace TechStack.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Route("Base")]
    public class BaseController : Controller
    {
#pragma warning disable SA1401 // Fields should be private
        /// <summary>
        /// Current user.
        /// </summary>
        public readonly int CurrentUserId;

        /// <summary>
        /// CurrentResourceId.
        /// </summary>
        public readonly int CurrentResourceId;
#pragma warning restore SA1401 // Fields should be private

        //private readonly ClaimsPrincipal tokenClaim;
        //private readonly HttpContext httpContext;
        //private readonly IClientConfiguration clientConfig;

        ///// <summary>
        /////  Initializes a new instance of the <see cref="BaseController"/> class.
        ///// </summary>
        ///// <param name="httpContextAccessor">httpContextAccessor</param>
        //public BaseController(IHttpContextAccessor httpContextAccessor)
        //{
        //    this.tokenClaim = httpContextAccessor.HttpContext.User;
        //    this.httpContext = httpContextAccessor.HttpContext;

        //    //this.CurrentUserId = (int)Enums.CurrentUserId.CurrentUser;
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        public BaseController()
        {
        }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="BaseController"/> class.
        ///// </summary>
        ///// <param name="clientConfig">clientConfig.</param>
        //public BaseController(IClientConfiguration clientConfig)
        //{
        //    this.CurrentUserId = clientConfig.CurrentUserId;
        //    this.CurrentResourceId = clientConfig.CurrentResourceId;
        //}

        /// <summary>
        /// SuccessResponse.
        /// </summary>
        /// <returns>Success Response.</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("SuccessResponse")]
        public IActionResult SuccessResponse()
        {
            return this.SuccessResponse(Constants.APISuccessText);
        }

        /// <summary>
        /// Success Response.
        /// </summary>
        /// <param name="message">pass message.</param>
        /// <returns>SuccessResponse.</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("SuccessResponseWithCustomMessage")]
        public IActionResult SuccessResponse(string message)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            resultModel.Status = Constants.APISuccessCode;
            resultModel.Message = Constants.APISuccessText;
            if (!string.IsNullOrEmpty(message))
            {
                resultModel.Message = message;
            }

            resultModel.Result = null;
            return this.Ok(resultModel);
        }

        /// <summary>
        /// Success Response.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="responseObject">response Object.</param>
        /// <returns>Returns Object.</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("SuccessResponseWithResponseObject")]
        public IActionResult SuccessResponse<T>(T responseObject)
        {
            return this.SuccessResponse(Constants.APISuccessText, responseObject);
        }

        /// <summary>
        /// Success Response.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="message">message.</param>
        /// <param name="responseObject">responseObject.</param>
        /// <returns>ResultModel.</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("SuccessResponseWithMessageAndResponseObject")]
        public IActionResult SuccessResponse<T>(string message, T responseObject)
        {
            ResultModel<T> resultModel = new ResultModel<T>();
            resultModel.Status = Constants.APISuccessCode;
            resultModel.Message = message;
            resultModel.Result = responseObject;
            return this.Ok(resultModel);
        }

        /// <summary>
        /// Success Paged Response.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="responseObject">Response Object.</param>
        /// <param name="totalRecords">Total Records.</param>
        /// <param name="currentPageNo">Current Page No.</param>
        /// <param name="pageSize">PageSize.</param>
        /// <param name="message">message.</param>
        /// <returns>SuccessPagedResponseWithResponseObject.</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("SuccessPagedResponseWithResponseObject")]
        public IActionResult SuccessPagedResponse<T>(T responseObject, int totalRecords, int currentPageNo, int pageSize, string message = "")
        {
            return this.SuccessPagedResponse(string.IsNullOrEmpty(message) ? Constants.APISuccessText : message, responseObject, totalRecords, currentPageNo, pageSize);
        }

        /// <summary>
        /// Success Paged Response.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="responseObject">Response Object.</param>
        /// <param name="totalRecords">Total Records.</param>
        /// <param name="currentPageNo">Current Page No.</param>
        /// <param name="pageSize">PageSize.</param>
        /// <returns>SuccessPagedResponseWithResponseObject.</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("SuccessPagedResponseWithResponseObject")]
        public IActionResult SuccessPagedResponse<T>(T responseObject, int totalRecords, int currentPageNo, int pageSize)
        {
            return SuccessPagedResponse(Constants.APISuccessText, responseObject, totalRecords, currentPageNo, pageSize);
        }

        /// <summary>
        /// Success Paged Response.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="message">Message.</param>
        /// <param name="responseObject">Response Object.</param>
        /// <param name="totalRecords">Total Records.</param>
        /// <param name="currentPageNo">Current Page No.</param>
        /// <param name="pageSize">PageSize.</param>
        /// <returns>SuccessPagedResponseWithMessageAndResponseObject.</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("SuccessPagedResponseWithMessageAndResponseObject")]
        public IActionResult SuccessPagedResponse<T>(string message, T responseObject, int totalRecords, int currentPageNo, int pageSize)
        {
            PagedResultModel<T> resultModel = new PagedResultModel<T>();
            resultModel.Status = Constants.APISuccessCode;
            resultModel.Message = message; // message;
            resultModel.Result = responseObject;

            if (currentPageNo <= 0)
            {
                currentPageNo = 1;
            }

            resultModel.CurrentPageNo = currentPageNo;
            resultModel.PageSize = pageSize;
            resultModel.TotalRecords = totalRecords;
            resultModel.NextPageNo = -1;

            if (totalRecords > 0)
            {
                decimal pageNos = (decimal)totalRecords / (decimal)pageSize;
                int totalNoOfPages = Convert.ToInt32(Math.Ceiling(pageNos));

                if (currentPageNo < totalNoOfPages)
                {
                    resultModel.NextPageNo = currentPageNo + 1;
                }
            }

            return this.Ok(resultModel);
        }

        /// <summary>
        /// FailureResponse.
        /// </summary>
        /// <returns>Failure Response.</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("FailureResponse")]
        public IActionResult FailureResponse()
        {
            return FailureResponse(Constants.APIFailureText);
        }

        /// <summary>
        /// FailureResponse.
        /// </summary>
        /// <param name="message">message.</param>
        /// <returns>status.</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("FailureResponseWithCustomMessage")]
        public IActionResult FailureResponse(string message)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            resultModel.Status = Constants.APIFailureCode;
            resultModel.Message = message;
            resultModel.Result = null;

            // In web(angular) it is not able to parse the message if status code is not 200 so commenting the old implementation
            // return this.StatusCode(500, resultModel);
            return Ok(resultModel);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("FailureFileResponseWithCustomMessage")]
        public ActionResult FailureFileResponse(string message)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            resultModel.Status = Constants.APIFailureCode;
            resultModel.Message = message;
            resultModel.Result = null;

            // In web(angular) it is not able to parse the message if status code is not 200 so commenting the old implementation
            // return this.StatusCode(500, resultModel);
            return NotFound(resultModel);
        }

        /// <summary>
        /// APIExceptionResponse.
        /// </summary>
        /// <param name="ex">ex.</param>
        /// <returns>returns exception.</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("ExceptionResponse")]
        public IActionResult APIExceptionResponse(Exception ex)
        {
            string originalErrorMessage = ex.Message;
            string localizedErrorMessage = ex.Message;

            // if error message and localized are same it means we were not aware of that error and not added in resource file.
            if (originalErrorMessage == localizedErrorMessage)
            {
                localizedErrorMessage = Constants.APIErrorCodesUnhandledException;
            }

            // Fetching error code (if any) from stored procedures.
            if (ex.InnerException != null)
            {
                if (ex.InnerException.GetType() == typeof(Microsoft.Data.SqlClient.SqlException))
                {
                    SqlException sqlEx = (SqlException)ex.InnerException;

                    if (sqlEx.Class.ToString() == Constants.SqlException && sqlEx.Number == Constants.SqExceptionRaisErrorNumber)
                    {
                        localizedErrorMessage = sqlEx.Message;
                    }
                }
            }

            ResultModel<object> resultModel = new ResultModel<object>();
            resultModel.Status = Constants.APIFailureCode;
            resultModel.Message = localizedErrorMessage;
            resultModel.Result = null;

            // In web(angular) its is not able to parse the message if status code is not 200 so commenting the old implementation
            // return this.StatusCode(500, resultModel);
            return this.Ok(resultModel);
        }

        ///// <summary>
        ///// Gets CurrentUserId
        ///// </summary>
        //public int CurrentUserId
        //{
        //    get
        //    {
        //        int currentUserId = 0;
        //        try
        //        {
        //            if (tokenClaim.HasClaim(c => c.Type == JwtRegisteredClaimNames.Sid))
        //            {
        //                currentUserId = int.Parse(tokenClaim.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid).Value);
        //            }
        //        }
        //        catch
        //        {
        //        }

        //        return currentUserId;
        //    }
        //}

        ///// <summary>
        ///// Gets get CultureInfo
        ///// </summary>
        //public CultureModel CultureInfo
        //{
        //    get
        //    {
        //        CultureModel cultureInfo = null;
        //        try
        //        {
        //            cultureInfo = new CultureModel()
        //            {
        //                Id = 1,
        //                Code = "en-GB",
        //                Type = Enums.CultureType.Current
        //            };
        //        }
        //        catch
        //        {
        //        }

        //        return cultureInfo;
        //    }
        //}

        ///// <summary>
        ///// Gets Id of logged-in user.
        ///// </summary>
        //protected string UserId
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(_userId))
        //        {
        //            ReadUserDataFromHeader();
        //        }

        //        return _userId;
        //    }
        //}

        ///// <summary>
        ///// Gets type of loged-in user.
        ///// </summary>
        //protected UserType UserType
        //{
        //    get
        //    {
        //        ReadUserDataFromHeader();

        //        //if (_userType == UserType.AnonymousUser)
        //        //{
        //        //    ReadUserDataFromHeader();
        //        //}
        //        return _userType;
        //    }
        //}

        ///// <summary>
        ///// Gets the type of device from which the request is triggered.
        ///// </summary>
        //protected DeviceTypes DeviceType
        //{
        //    get
        //    {
        //        ReadUserDataFromHeader();

        //        //if (_deviceType == DeviceTypes.Unknown)
        //        //{
        //        //    ReadUserDataFromHeader();
        //        //}
        //        return _deviceType;
        //    }
        //}

        ///// <summary>
        ///// Gets active login id of current user.
        ///// </summary>
        //protected Guid? LoginId
        //{
        //    get
        //    {
        //        ReadUserDataFromHeader();

        //        //if (!_loginId.HasValue)
        //        //{
        //        //    ReadUserDataFromHeader();
        //        //}
        //        return _loginId;
        //    }
        //}

        ///// <summary>
        ///// To check user is a logedin user.
        ///// </summary>
        ///// <returns>true if user is loged-in user and false otherwise.</returns>
        //protected bool IsUserLoggedIn()
        //{
        //    if (string.IsNullOrEmpty(UserId))
        //    {
        //        return false;
        //    }

        //    if (DeviceType == DeviceTypes.Unknown)
        //    {
        //        return false;
        //    }

        //    if (UserType == UserType.AnonymousUser)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        ///// <summary>
        ///// To set logged-in user details from header.
        ///// </summary>
        //private void ReadUserDataFromHeader()
        //{
        //    if (tokenClaim.HasClaim(c => c.Type == JwtRegisteredClaimNames.Sid))
        //    {
        //        _userId = tokenClaim.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid).Value;
        //    }
        //    else
        //    {
        //        _userId = "2f1f9da6-7d88-4e91-a9ba-fd70433108ce";
        //    }

        //    if (tokenClaim.HasClaim(c => c.Type == JwtRegisteredClaimNames.Iat))
        //    {
        //        _loginId = Guid.Parse(tokenClaim.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Iat).Value);
        //    }
        //    else
        //    {
        //        _loginId = Guid.NewGuid();
        //    }

        //    if (tokenClaim.HasClaim(c => c.Type == ClaimTypes.Role))
        //    {
        //        var userType = tokenClaim.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
        //        if (!string.IsNullOrEmpty(userType))
        //        {
        //            Enum.TryParse(userType, out UserType uType);
        //            this._userType = uType;
        //        }
        //        else
        //        {
        //            this._userType = UserType.RegisteredUser;
        //        }
        //    }
        //    else
        //    {
        //        this._userType = UserType.RegisteredUser;
        //    }

        //    var deviceType = httpContext.Request.Headers["DeviceType"].FirstOrDefault();
        //    if (!string.IsNullOrEmpty(deviceType))
        //    {
        //        Enum.TryParse(deviceType, out DeviceTypes dType);
        //        this._deviceType = dType;
        //    }
        //    else
        //    {
        //        this._deviceType = DeviceTypes.Web;
        //    }
        //}
    }
}
