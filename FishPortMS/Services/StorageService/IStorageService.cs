using FishPortMS.Shared.Enums;

namespace FishPortMS.Services.StorageService
{
    public interface IStorageService
    {
        Task SaveToLocal<T>(LookupKey key, T data);
        Task<T?> GetFromLocal<T>(LookupKey key);
        Task RemoveFromLocal(LookupKey key);
    }
}
