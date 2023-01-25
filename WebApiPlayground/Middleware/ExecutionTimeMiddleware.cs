using System.Diagnostics;

namespace WebApiPlayground.Middleware
{
    public class ExecutionTimeMiddleware : IMiddleware
    {
        private readonly ILogger<ExecutionTimeMiddleware> _logger;

        public ExecutionTimeMiddleware(ILogger<ExecutionTimeMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                await next(context);
            }
            finally
            {
                if (stopwatch.Elapsed > TimeSpan.FromSeconds(1))
                {
                    _logger.LogInformation($"Query: {context.Request.Path}, execution time: {stopwatch.Elapsed.TotalSeconds:0.00}s");
                }
            }
        }
    }
}