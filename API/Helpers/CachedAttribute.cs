using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Helpers
{
     
    /// <summary>
    /// Caching for static content endpoints
    /// </summary>
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveInSeconds;

        public CachedAttribute(int timeToLiveInSeconds)
        {
            this._timeToLiveInSeconds = timeToLiveInSeconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext
                .RequestServices.GetRequiredService<IResponseCacheService>();
            string cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cashedResponse = await cacheService.GetCahchedResponseAsync(cacheKey);

            if(! string.IsNullOrEmpty(cashedResponse))
            {
                var contentResult = new ContentResult()
                {
                    Content = cashedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult;
                return;
            }

            var executedContext = await next(); // execute the controller code
            if(executedContext.Result is OkObjectResult res)
            {
                await cacheService.CacheResponseAsync(cacheKey, res, TimeSpan.FromSeconds(_timeToLiveInSeconds));
            }
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();

            keyBuilder.Append($"{request.Path}");
            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($":{key}-{value}");
            }
            return keyBuilder.ToString();
        }
    }
}
