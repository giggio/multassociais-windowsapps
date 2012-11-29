using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MultasSociais.Lib
{
    public static class Extensions
    {
        public static async Task<string> GetResponseAsync(this HttpWebRequest request)
        {
            var response = await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null);
            var responseContent = await new StreamReader(response.GetResponseStream()).ReadToEndAsync();
            return responseContent;
        }

        public static async Task<T> Obter<T>(this string url)
        {
            var request = WebRequest.CreateHttp(url);
            var responseContent = await request.GetResponseAsync();
            var multas = JsonConvert.DeserializeObject<T>(responseContent);
            return multas;
        }
    }
}