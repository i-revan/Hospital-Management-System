using HospitalManagementSystem.Application.Abstraction.Services;
using Microsoft.Extensions.Caching.Memory;

namespace HospitalManagementSystem.Persistence.Implementations.Services;

public class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _cache;

    public MemoryCacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> createItem, TimeSpan? absoluteExpirationRelativeToNow = null, TimeSpan? slidingExpiration = null)
    {
        return await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow ?? TimeSpan.FromMinutes(5);
            entry.SlidingExpiration = slidingExpiration ?? TimeSpan.FromMinutes(2);
            return await createItem();
        });
    }

    public void Remove(string cacheKey)
    {
        _cache.Remove(cacheKey);
    }
}
