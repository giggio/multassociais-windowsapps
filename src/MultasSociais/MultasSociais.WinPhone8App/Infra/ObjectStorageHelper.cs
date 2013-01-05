using System.IO;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MultasSociais.WinPhone8App.Infra
{
    public interface IObjectStorageHelper<T>
    {
        Task SaveAsync(T obj);
        Task<T> LoadAsync();
    }

    public class ObjectStorageHelper<T> : IObjectStorageHelper<T>
    {
        public async Task SaveAsync(T obj)
        {
            var fileName = GetFileName();
            var serializedObj = JsonConvert.SerializeObject(obj);
            using (var storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (storage.FileExists(fileName))
                    storage.DeleteFile(fileName);
                using (var stream = new IsolatedStorageFileStream(fileName, FileMode.CreateNew, storage))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        await writer.WriteAsync(serializedObj);
                    }
                }
            }
        }

        public async Task<T> LoadAsync()
        {
            var fileName = GetFileName();
            T obj;
            using (var storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!storage.FileExists(fileName))
                {
                    return default(T);
                }
                using (var stream = new IsolatedStorageFileStream(fileName, FileMode.Open, storage))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var serializedObj = await reader.ReadToEndAsync();
                        obj = JsonConvert.DeserializeObject<T>(serializedObj);
                    }
                }
            }
            return obj;
        }

        private static string GetFileName()
        {
            var fileName = typeof(T).FullName + ".js";
            return fileName;
        }

    }
}
