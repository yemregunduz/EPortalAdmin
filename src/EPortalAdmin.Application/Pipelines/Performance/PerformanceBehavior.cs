using EPortalAdmin.Core.Logging.Serilog;
using MediatR;
using System.Diagnostics;

namespace EPortalAdmin.Application.Pipelines.Performance
{
    public class PerformanceBehavior<TRequest, TResponse>(Stopwatch stopwatch, LoggerServiceBase loggerServiceBase) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, IIntervalRequest
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response;

            try
            {
                stopwatch.Start();
                response = await next();
            }
            finally
            {
                if (stopwatch.Elapsed.TotalSeconds > request.Interval)
                {
                    loggerServiceBase.Warn($"Performance -> {request.GetType().Name} {stopwatch.Elapsed.TotalSeconds}");
                }

                stopwatch.Restart();
            }

            return response;
        }
    }
}
