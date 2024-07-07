namespace HospitalManagementSystem.Application.Abstraction.Services;

public interface ICacheService
{
    Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> createItem, TimeSpan? absoluteExpirationRelativeToNow = null, TimeSpan? slidingExpiration = null);
    void Remove(string cacheKey);
}
