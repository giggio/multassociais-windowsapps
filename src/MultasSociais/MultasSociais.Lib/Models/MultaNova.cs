using System;
using Newtonsoft.Json;

namespace MultasSociais.Lib.Models
{
    public class MultaNova
    {
        [JsonProperty(PropertyName = "data_ocorrencia")]
        public DateTime DataOcorrencia { get; set; }
        [JsonProperty(PropertyName = "descricao")]
        public string Descricao { get; set; }
        [JsonProperty(PropertyName = "placa")]
        public string Placa { get; set; }
        [JsonProperty(PropertyName = "video")]
        public string VideoUrl { get; set; }
    }
}