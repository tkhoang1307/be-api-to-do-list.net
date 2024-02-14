using System.Net;

namespace ToDoList.Model.ResponseModels
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; private set; }
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; } = string.Empty;
        public object? Data { get; private set; }

        public ApiResponse SetOk(object? data)
        {
            IsSuccess = true;
            StatusCode = HttpStatusCode.OK;
            Data = data;
            return this;
        }

        public ApiResponse SetNotFound(object? data = null, string? message = null)
        {
            IsSuccess = false;
            StatusCode = HttpStatusCode.NotFound;
            if (!string.IsNullOrEmpty(message))
            {
                ErrorMessage = message;
            }
            Data = data;
            return this;
        }

        public ApiResponse SetBadRequest(object? data = null, string? message = null)
        {
            IsSuccess = false;
            StatusCode = HttpStatusCode.BadRequest;
            if (message != null)
            {
                ErrorMessage = message;
            }
            Data = data;
            return this;
        }

        public ApiResponse SetApiResponse(HttpStatusCode statusCode, bool isSuccess, string? message = null, object? data = null)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            if (message != null)
            {
                ErrorMessage = message;
            }
            Data = data;
            return this;
        }
    }
}
