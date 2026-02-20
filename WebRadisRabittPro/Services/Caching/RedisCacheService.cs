using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;

namespace WebRadisRabittPro.Services.Caching
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache? _cache;

        public RedisCacheService(IDistributedCache? cache)
        {
            _cache = cache;        
        }
        public T? GetData<T>(string Key)
        {
            var data = _cache?.GetString(Key);

            if(data is null)
                return default(T?);

            return JsonSerializer.Deserialize<T>(data);
        }

        public async Task<T?> GetDataAsync<T>(string key)
        {
            var data = await _cache.GetStringAsync(key);

            if (data == null)
                return default;

            return JsonSerializer.Deserialize<T>(data);
        }


        public void SetData<T>(string Key, T Value)
        {
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };

            _cache?.SetString(Key, JsonSerializer.Serialize(Value), options);
        }

        public async Task SetDataAsync<T>(string key, T value)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };

            var json = JsonSerializer.Serialize(value);

            await _cache.SetStringAsync(key, json, options);
        }

        public async Task RemoveDataAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
