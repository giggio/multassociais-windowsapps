using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MultasSociais.Lib
{
    public class HttpUpload
    {
        private readonly Encoding encoding = Encoding.UTF8;
        private readonly string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
        private Stream requestStream;
        private readonly HttpWebRequest request;

        public struct FileInfo
        {
            public string ParamName { get; set; }
            public string ContentType { get; set; }
            public string FileName { get; set; }
            public byte[] Buffer { get; set; }
        }

        public HttpUpload(string url)
        {
            request = WebRequest.CreateHttp(url);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
        }
        public async Task<HttpWebResponse> Upload(FileInfo fileInfo, Dictionary<string, string> values)
        {
            using (requestStream = await request.GetRequestStreamAsync())
            {
                foreach (var key in values.Keys)
                {
                    WriteBoundary();
                    const string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                    var formItem = string.Format(formdataTemplate, key, values[key]);
                    var formItemBytes = encoding.GetBytes(formItem);
                    requestStream.Write(formItemBytes, 0, formItemBytes.Length);
                }
                WriteBoundary();

                WriteFile(fileInfo);

                WriteTrailer();
            }
            var response = await request.GetResponseAsync();
            return response;
        }

        private void WriteTrailer()
        {
            var trailer = encoding.GetBytes("\r\n--" + boundary + "--\r\n");
            requestStream.Write(trailer, 0, trailer.Length);
        }

        private void WriteFile(FileInfo fileInfo)
        {
            var header = string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n",
                              fileInfo.ParamName, fileInfo.FileName, fileInfo.ContentType);
            var headerBytes = encoding.GetBytes(header);
            requestStream.Write(headerBytes, 0, headerBytes.Length);
            requestStream.Write(fileInfo.Buffer, 0, fileInfo.Buffer.Length);
        }

        private void WriteBoundary()
        {
            var boundaryBytes = encoding.GetBytes("\r\n--" + boundary + "\r\n");
            requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
        }
    }
}