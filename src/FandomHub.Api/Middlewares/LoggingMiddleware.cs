using Microsoft.IdentityModel.Logging;
using System.Text.Json;

namespace FandomHub.Api.Middlewares
{
	public class LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
	{
		private readonly RequestDelegate _next = next;
		private readonly ILogger<LoggingMiddleware> _logger = logger;
		private const long MaxLogContentLength = 10_000_000; // 10 MB

		public async Task InvokeAsync(HttpContext context)
		{
			// Log request
			await LogRequestAsync(context);

			// Capture response
			var originalResponseBodyStream = context.Response.Body;
			using var responseBodyStream = new MemoryStream();
			context.Response.Body = responseBodyStream;

			try
			{
				await _next(context);

				// Log response
				await LogResponseAsync(context);
			}
			finally
			{
				// Copy response back to original stream
				responseBodyStream.Seek(0, SeekOrigin.Begin);
				await responseBodyStream.CopyToAsync(originalResponseBodyStream);
				context.Response.Body = originalResponseBodyStream;
			}
		}

		private async Task LogRequestAsync(HttpContext context)
		{
			var request = context.Request;

			// Đọc request body nếu có
			string requestBody = string.Empty;
			if (request.ContentLength > 0 && request.Body.CanSeek)
			{
				request.Body.Seek(0, SeekOrigin.Begin);
				using var reader = new StreamReader(request.Body, leaveOpen: true);
				requestBody = await reader.ReadToEndAsync();
				request.Body.Seek(0, SeekOrigin.Begin);
			}

			_logger.LogInformation("HTTP Request: {Method} {Path} {QueryString} - Body: {Body}",
				request.Method,
				request.Path,
				request.QueryString,
				string.IsNullOrEmpty(requestBody) ? "Empty" : requestBody);
		}

		private async Task LogResponseAsync(HttpContext context)
		{
			var response = context.Response;

			// Đọc response body
			string responseBody = string.Empty;
			if (response.Body.CanSeek && response.Body.Length > 0)
			{
				response.Body.Seek(0, SeekOrigin.Begin);
				using var reader = new StreamReader(response.Body, leaveOpen: true);
				responseBody = await reader.ReadToEndAsync();
				response.Body.Seek(0, SeekOrigin.Begin);
			}

			_logger.LogInformation("HTTP Response: {StatusCode} - Body: {Body}",
				response.StatusCode,
				string.IsNullOrEmpty(responseBody) ? "Empty" : responseBody);
		}
	}
}
