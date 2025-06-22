using System.Text.Json;

namespace FandomHub.Api.Middlewares
{
	public class GlobalExceptionMiddleware(ILoggerFactory loggerFactory) : IMiddleware
	{
		private readonly ILogger<GlobalExceptionMiddleware> _logger = loggerFactory.CreateLogger<GlobalExceptionMiddleware>();

		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unhandled exception occurred");

				context.Response.StatusCode = 500;
				context.Response.ContentType = "application/json";

				var result = JsonSerializer.Serialize(new
				{
					Error = "Something went wrong",
					Detail = ex.Message // Đừng để full `ToString()` trong production
				});
				await context.Response.WriteAsync(result);
			}
		}
	}

}
