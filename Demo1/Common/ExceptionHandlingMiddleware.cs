using Demo1.Infrastructure;

namespace Demo1.Api.Common
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var response = ApiResponse<string>.FailResponse(
                    new List<string> { ex.Message }, "Internal Server Error");
                await context.Response.WriteAsJsonAsync(response);
            }
        }

}

}
