using Talabat.APIs.Errors;

namespace Talabat.APIs.Middlewares;

public class ExceptionMiddleWare
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionMiddleWare> logger;
    private readonly IHostEnvironment environment;

    public ExceptionMiddleWare(RequestDelegate next,ILogger<ExceptionMiddleWare> logger ,IHostEnvironment environment) {
        this.next = next;
        this.logger = logger;
        this.environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred.");
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            //var response = environment.IsDevelopment()
            //    ? new { message = ex.Message, stackTrace = ex.StackTrace }
            //    : new { message = "An unexpected error occurred." };
             
            var response = environment.IsDevelopment()
                ? new ApiExceptionResponse(500, ex.Message, ex.StackTrace?.ToString())
                : new ApiExceptionResponse(500, "An unexpected error occurred.");





            await context.Response.WriteAsJsonAsync(response);
        }
    }
}