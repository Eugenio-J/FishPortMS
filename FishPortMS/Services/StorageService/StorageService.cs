using Blazored.LocalStorage;
using FishPortMS.Shared.Enums;
using Microsoft.JSInterop;

namespace FishPortMS.Services.StorageService
{
    public class StorageService : IStorageService
    {
        private readonly ILocalStorageService _localstorageService;
        private readonly IJSRuntime _jsRuntime;
        public StorageService(ILocalStorageService localstorageService, IJSRuntime jsRuntime)
        {
            _localstorageService = localstorageService;
            _jsRuntime = jsRuntime;
        }

        public async Task<T?> GetFromLocal<T>(LookupKey key)
        {
            try
            {
                return await _localstorageService.GetItemAsync<T>(key.ToString());
            }

            catch (InvalidOperationException ex) 
            {
                if (ex.Message.Contains("Javascript interop")) 
                {
                    var item = await _jsRuntime.InvokeAsync<T>("getLocalStorageItem", key.ToString());
                    return item;
                }
                throw ex;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RemoveFromLocal(LookupKey key)
        {
           await _localstorageService.RemoveItemAsync(key.ToString());
        }

        public async Task SaveToLocal<T>(LookupKey key, T data)
        {
            if (data != null) 
            {
                await _localstorageService.SetItemAsync(key.ToString(), data);
            }
        }
    }
}
