using EnterpriseInventory.Application.Common;

namespace EnterpriseInventory.API.Helpers;

public static class ApiResponseFactory
{
    public static ApiResponse<T> Success<T>(T data,string message,int statusCode,string traceId)
    {
        return new ApiResponse<T>
        {
            Success = true,
            StatusCode = statusCode,
            Message = message,
            Data = data,
            TraceId = traceId
        };
    }
}