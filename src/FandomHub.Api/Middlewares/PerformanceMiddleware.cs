using System.Diagnostics;

namespace FandomHub.Api.Middlewares
{
	public class PerformanceMiddleware(Stopwatch stopwatch, ILoggerFactory loggerFactory) : IMiddleware
	{
		private readonly Stopwatch _stopwatch = stopwatch;
		private readonly ILogger<PerformanceMiddleware> _logger = loggerFactory.CreateLogger<PerformanceMiddleware>();

		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			_stopwatch.Restart();
			_stopwatch.Start();

			await next(context);

			_stopwatch.Stop();
			TimeSpan timeTaken = _stopwatch.Elapsed;

			_logger.LogInformation("Time taken: {timeTaken}", timeTaken.ToString(@"m\:ss\.fff"));
		}
	}
}
