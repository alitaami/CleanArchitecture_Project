using Application.Features.Behaviors.Contracts;
using Application.Models;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Application.Features.Behaviors
{
    public class RamovalCachePipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, IRemovalCacheable
    {
        private readonly IDistributedCache _cache;
        private readonly CacheSettings _cacheSettings;
        public RamovalCachePipeline(IDistributedCache cache, IOptions<CacheSettings> cachesettings)
        {
            _cache = cache;
            _cacheSettings = cachesettings.Value;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request.ByPassCache) return await next();

            TResponse response;
            string cacheKey = $"{_cacheSettings.ApplicationName}:{request.CacheKey}";

            var cahceResponse = await _cache.GetAsync(cacheKey, cancellationToken);

            if (cahceResponse != null)
            {
                await _cache.RemoveAsync(cacheKey, cancellationToken); 
            }
            // Continue with the request pipeline
            response = await next();

            return response;
        }
    }
}

