using Application.Features.Behaviors.Contracts;
using Application.Models;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace Application.Features.Behaviors
{
    public class CachePipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICacheable
    {
        private readonly IDistributedCache _cache;
        private readonly CacheSettings _cacheSettings;
        public CachePipeline(IDistributedCache cache, IOptions<CacheSettings> cachesettings)
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
                response = JsonConvert.DeserializeObject<TResponse>(Encoding.Default.GetString(cahceResponse));
            }
            else
            {
                // Get the response and write to cahce
                response = await GetResponseAndWriteToCahceAsync();

            }
            return response;

            // local method for setData
            async Task<TResponse> GetResponseAndWriteToCahceAsync()
            {
                response = await next();

                if (response != null)
                {
                    var slidingExpiration = request.SlidingExpiration == null ?
                        TimeSpan.FromMinutes(_cacheSettings.SlidingExpiration) : request.SlidingExpiration;

                    var cahceOptions = new DistributedCacheEntryOptions
                    {
                        SlidingExpiration = slidingExpiration,
                        AbsoluteExpiration = DateTime.Now.AddDays(1)
                    };
                }


                var serializedData = Encoding.Default
                    .GetBytes(
                    JsonConvert
                    .SerializeObject(response, Newtonsoft.Json.Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    }));

                await _cache.SetAsync(cacheKey, serializedData);

                return response;
            }
        }
    }
}

