using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace MultasSociais.WinStoreApp
{
    public static class Entensoes
    {
        public async static Task<Byte[]> GetByteFromFileAsync(this StorageFile storageFile)
        {
            var stream = await storageFile.OpenReadAsync();
            var bytes = await stream.GetByteFromFileAsync();
            return bytes;
        }
        public async static Task<Byte[]> GetByteFromFileAsync(this IRandomAccessStream stream)
        {
            using (var dataReader = new DataReader(stream))
            {
                var bytes = new byte[stream.Size];
                await dataReader.LoadAsync((uint)stream.Size);
                dataReader.ReadBytes(bytes);
                return bytes;
            }
        }
    }
}
