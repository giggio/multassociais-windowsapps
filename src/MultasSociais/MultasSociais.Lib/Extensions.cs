using System.IO;
using System.Net;
using System.Threading.Tasks;
using MultasSociais.Lib.Models;
using Newtonsoft.Json;

namespace MultasSociais.Lib
{
    public static class Extensions
    {
        public static async Task<Stream> GetResponseStreamAsync(this string url)
        {
            var request = WebRequest.CreateHttp(url);
            var response = await request.GetResponseAsync();
            var stream = response.GetResponseStream();
            return stream;
        }

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
            //o pedido da stream é só para setar o content length em 0, porque senao o servidor falha. e a prop contentlength nao esta disponivel em PCL.
            await request.GetRequestStreamAsync();
            var response = await request.GetResponseAsync();
            return response.StatusCode;
        }
        
        public static async Task<HttpWebResponse> Postar<T>(this string url, T item, byte[] fileBuffer)
        {
            var request = WebRequest.CreateHttp(url);
            request.Method = "POST";
            var requestStream = await request.GetRequestStreamAsync();
            var writer = new StreamWriter(requestStream);
            var itemSerializado = JsonConvert.SerializeObject(item);
            await writer.WriteAsync(itemSerializado);
            var response = await request.GetResponseAsync();
            return response;
        }

        public static async Task<Stream> GetRequestStreamAsync(this HttpWebRequest request)
        {
            var stream = await Task.Factory.FromAsync<Stream>(request.BeginGetRequestStream, request.EndGetRequestStream, null);
            return stream;
        }



    }
}