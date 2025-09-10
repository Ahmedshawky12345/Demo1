namespace Demo1.Api.Common
{
    public class ApiResponse<T>  where  T: class
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public ApiResponse(bool success, string message, T data,List<string> errors=null)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = errors;
        }
        public static ApiResponse<T> SuccessResponse(bool success,T data,string message="")
        {
          return new ApiResponse<T>(true,message,data);
        }
        public static ApiResponse<T> FailResponse(List<string>errors,string message ="")
        {
            return new ApiResponse<T>(false, message, default, errors);
        }
    }
}
