namespace Domain.Common;

public class BaseResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = new();
    public int StatusCode { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public static BaseResponse<T> SuccessResult(T data, string message = "Success")
    {
        return new BaseResponse<T>
        {
            Success = true,
            Message = message,
            Data = data,
            StatusCode = 200
        };
    }

    public static BaseResponse<T> ErrorResult(string message, int statusCode = 400)
    {
        return new BaseResponse<T>
        {
            Success = false,
            Message = message,
            StatusCode = statusCode
        };
    }

    public static BaseResponse<T> ErrorResult(List<string> errors, string message = "Validation failed", int statusCode = 400)
    {
        return new BaseResponse<T>
        {
            Success = false,
            Message = message,
            Errors = errors,
            StatusCode = statusCode
        };
    }
}

public class BaseResponse : BaseResponse<object>
{
    public static BaseResponse SuccessResult(string message = "Success")
    {
        return new BaseResponse
        {
            Success = true,
            Message = message,
            StatusCode = 200
        };
    }

    public new static BaseResponse ErrorResult(string message, int statusCode = 400)
    {
        return new BaseResponse
        {
            Success = false,
            Message = message,
            StatusCode = statusCode
        };
    }
}
