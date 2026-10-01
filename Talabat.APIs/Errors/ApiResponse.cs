
namespace Talabat.APIs.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string? message =null)
        {
            this.statusCode = statusCode;
            this.message = message ?? GetDefaultMessage(statusCode);
        }

        private string? GetDefaultMessage(int? statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "Unauthorized",
                404 => "Resource Not Found",
                500 => "Internal Server Error",
                _ => null

            };
        }

        public int  statusCode { get; set; }
        public string ? message { get; set; }
        
    }
}
