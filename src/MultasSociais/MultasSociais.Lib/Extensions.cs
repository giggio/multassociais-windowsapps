using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MultasSociais.Lib
{
    public static class Extensions
    {
        public static async Task<string> GetResponseContentAsync(this HttpWebRequest request)
        {
            var response = await request.GetResponseAsync();
            var responseContent = await new StreamReader(response.GetResponseStream()).ReadToEndAsync();
            return responseContent;
        }

        public static async Task<HttpWebResponse> GetResponseAsync(this HttpWebRequest request)
        {
            var response = (HttpWebResponse)await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null);
            return response;
        }

        public static async Task<T> Obter<T>(this string url)
        {
            var request = WebRequest.CreateHttp(url);
            var responseContent = await request.GetResponseContentAsync();
            var multas = JsonConvert.DeserializeObject<T>(responseContent);
            return multas;
        }

        public static async Task<HttpStatusCode> Postar(this string url)
        {
            var request = WebRequest.CreateHttp(url);
            request.Method = "POST";
            var response = await request.GetResponseAsync();
            return response.StatusCode;
        }

        private static async Task<Stream> GetRquestStreamAsync(HttpWebRequest request)
        {
            var stream = await Task.Factory.FromAsync<Stream>(request.BeginGetRequestStream, request.EndGetRequestStream, null);
            return stream;
        }
    }
}