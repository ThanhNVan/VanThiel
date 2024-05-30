namespace VanThiel.SharedLibrary.Entity;

public class ApiResult<TData>
    where TData : class
{
    #region [ Ctor ]
    public ApiResult()
    {

    }

    public ApiResult(string statusCode, string message, TData data)
    {
        StatusCode = statusCode;
        Message = message;
        Data = data;
    }
    #endregion

    #region
    public string StatusCode { get; set; }

    public string Message { get; set; }

    public TData Data { get; set; }
    #endregion
}
