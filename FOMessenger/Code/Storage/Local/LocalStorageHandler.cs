using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            Task task = new Task(async () => await StoreValueAsync<T>(value, keySuffix));
            task.Start();
            task.Wait();
        }

        public async Task StoreValueAsync<T>(T value, string keySuffix)
        {
            await _protectedLocalStorage.SetAsync(_storageKey + "-" + keySuffix, JsonConvert.SerializeObject(value));
        }


        public void StoreValues<T>(IEnumerable<T> values, string keySuffix)
        {
            Task task = new Task(async () => await StoreValuesAsync<T>(values, keySuffix));
            task.Start();
            task.Wait();
        }

        public async Task StoreValuesAsync<T>(IEnumerable<T> values, string keySuffix)
        {
            List<T> list = values.ToList();

            int pos = 0;

            while ((await _protectedLocalStorage.GetAsync<string>(_storageKey + "-" + keySuffix + "-" + pos)).Value != null)
            {
                await _protectedLocalStorage.DeleteAsync(_storageKey + "-" + keySuffix + "-" + pos);
                pos++;
            }

            for (int i = 0; i < list.Count; i++)
            {
                await _protectedLocalStorage.SetAsync(_storageKey + "-" + keySuffix + "-" + i, JsonConvert.SerializeObject(list[i]));
            }
        }


        public T RetrieveValue<T>(string keySuffix)
        {
            return RetrieveValueAsync<T>(keySuffix).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public async Task<T> RetrieveValueAsync<T>(string keySuffix)
        {
            Task<string?> task = new Task<string?>(() => _protectedLocalStorage.GetAsync<string?>(_storageKey + "-" + keySuffix).ConfigureAwait(false).GetAwaiter().GetResult().Value);
            task.Start();
            task.Wait();
            string? CurrentString = task.Result;
            return JsonConvert.DeserializeObject<T>(CurrentString);
        }


        public IEnumerable<T> RetrieveValues<T>(string keySuffix)
        {
            Task<IEnumerable<T>> task = new Task<IEnumerable<T>>(() => RetrieveValuesAsync<T>(keySuffix).ConfigureAwait(false).GetAwaiter().GetResult());
            task.Start();
            task.Wait();
            return task.Result;
        }

        public async Task<IEnumerable<T>> RetrieveValuesAsync<T>(string keySuffix)
        {
            List<T> values = new List<T>();
            int pos = -1;

            while (true)
            {
                pos++;
                string? CurrentString = (await _protectedLocalStorage.GetAsync<string?>(_storageKey + "-" + keySuffix + "-" + pos)).Value;
                if (CurrentString == null)
                {
                    break;
                }
                values.Add(JsonConvert.DeserializeObject<T>(CurrentString));
            }
            return values;
        }
    }
}
