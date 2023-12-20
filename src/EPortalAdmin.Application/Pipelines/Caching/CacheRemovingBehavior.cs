using EPortalAdmin.Core.Logging.Serilog;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace EPortalAdmin.Application.Pipelines.Caching
{
    public class CacheRemovingBehavior<TRequest, TResponse>(IDistributedCache cache, LoggerServiceBase logger) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICacheRemoverRequest
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request.BypassCache)
                return await next();

            TResponse response = await next();

            if (request.CacheGroupKey != null)
            {
                byte[]? cachedGroup = await cache.GetAsync(request.CacheGroupKey, cancellationToken);
                if (cachedGroup != null)
                {
                    HashSet<string> keysInGroup = JsonSerializer.Deserialize<HashSet<string>>(Encoding.Default.GetString(cachedGroup))!;
                    foreach (string key in keysInGroup)
                    {
                        await cache.RemoveAsync(key, cancellationToken);
                        logger.Info($"Removed Cache -> {key}");
                    }

                    await cache.RemoveAsync(request.CacheGroupKey, cancellationToken);
                    logger.Info($"Removed Cache -> {request.CacheGroupKey}");
                    await cache.RemoveAsync(key: $"{request.CacheGroupKey}SlidingExpiration", cancellationToken);
                    logger.Info($"Removed Cache -> {request.CacheGroupKey}SlidingExpiration");
                }
            }

            if (request.CacheKey != null)
            {
                await cache.RemoveAsync(request.CacheKey, cancellationToken);
                logger.Info($"Removed Cache -> {request.CacheKey}");
            }

            return response;
        }
    }
}

