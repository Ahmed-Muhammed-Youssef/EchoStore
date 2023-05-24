using System;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);
        Task<string> GetCahchedResponseAsync(string cacheKey);
    }
}
