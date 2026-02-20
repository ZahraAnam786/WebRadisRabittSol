namespace WebRadisRabittPro.Services.Caching
{
    public interface IRedisCacheService
    {
        T? GetData<T> (string Key);
        Task<T?> GetDataAsync<T>(string key);
        void SetData<T>(string Key, T Value);
        Task SetDataAsync<T>(string key, T value);
        Task RemoveDataAsync(string key);
    }
}
