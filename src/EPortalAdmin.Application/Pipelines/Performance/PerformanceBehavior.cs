using MediatR;
using EPortalAdmin.Core.Logging.Serilog;
using System.Diagnostics;

namespace EPortalAdmin.Application.Pipelines.Performance
{
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, IIntervalRequest
    {
        private readonly Stopwatch _stopwatch;
        private readonly LoggerServiceBase _loggerService;

        public PerformanceBehavior(Stopwatch stopwatch,LoggerServiceBase loggerServiceBase)
        {
            _stopwatch = stopwatch;
            _loggerService = loggerServiceBase;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response;

            try
            {
                _stopwatch.Start();
                response = await next();
            }
            finally
            {
                if(_stopwatch.Elapsed.TotalSeconds > request.Interval)
                {
                    _loggerService.Info($"Performance -> {request.GetType().Name} {_stopwatch.Elapsed.TotalSeconds}");
                }

                _stopwatch.Restart();
            }

            return response;
        }
    }
}
