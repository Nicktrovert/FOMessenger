using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;

namespace FOMessenger.Code.Storage.Local
{
    public class LocalStorageHandler
    {
        private readonly ProtectedLocalStorage _protectedLocalStorage;
        private readonly string _storageKey = "wabadabadubdub";

        public LocalStorageHandler(ProtectedLocalStorage protectedLocalStorage)
        {
            _protectedLocalStorage = protectedLocalStorage ?? throw new ArgumentNullException(nameof(protectedLocalStorage));
        }




        public void StoreValue<T>(T value, string keySuffix)
        {
            _protectedLocalStorage.SetAsync(_storageKey + "-" + keySuffix, JsonConvert.SerializeObject(value));
        }

        public async Task StoreValueAsync<T>(T value, string keySuffix) => await Task.Run(() => StoreValue(value, keySuffix));


        public void StoreValues<T>(IEnumerable<T> values, string keySuffix)
        {
            List<T> list = values.ToList();

            int pos = -1;

            do
            {
                pos++;
                _protectedLocalStorage.DeleteAsync(_storageKey + "-" + keySuffix + "-" + pos);
            } while (_protectedLocalStorage.GetAsync<string>(_storageKey + "-" + keySuffix + "-" + (pos + 1)).Result.Value != null);

            for (int i = 0; i < list.Count; i++)
            {
                _protectedLocalStorage.SetAsync(_storageKey + "-" + keySuffix + "-" + i, JsonConvert.SerializeObject(list[i]));
            }
        }

        public async Task StoreValuesAsync<T>(IEnumerable<T> values, string keySuffix) => await Task.Run(() => StoreValues(values, keySuffix));




        public T RetrieveValue<T>(string keySuffix)
        {
            throw new NotImplementedException("todo");
        }

        public async Task<T> RetrieveValueAsync<T>(string keySuffix) => await Task.Run(() => RetrieveValue<T>(keySuffix));


        public IEnumerable<T> RetrieveValues<T>(string keySuffix)
        {
            throw new NotImplementedException("todo");
        }

        public async Task<IEnumerable<T>> RetrieveValuesAsync<T>(string keySuffix) => await Task.Run(() => RetrieveValues<T>(keySuffix));


    }
}
