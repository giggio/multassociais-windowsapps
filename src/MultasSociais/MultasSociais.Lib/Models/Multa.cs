using System;
using Newtonsoft.Json;

namespace MultasSociais.Lib.Models
{
    public class Multa
    {
        public int Id { get; set; }
        [JsonProperty(PropertyName = "data_ocorrencia")]
        public DateTime DataOcorrencia { get; set; }
        public string Descricao { get; set; }
        public string Placa { get; set; }
        [JsonProperty(PropertyName = "likes")]
        public int NumeroDeMultas { get; set; }
        [JsonProperty(PropertyName = "video")]
        public string VideoUrl { get; set; }
        [JsonProperty(PropertyName = "foto_url")]
        public string FotoUrl { get; set; }
        public GrupoDeMultas Grupo { get; set; }

        public string DataDescrita
        {
            get { return Math.Round(DateTime.UtcNow.Subtract(DataOcorrencia).TotalDays, 0) + " dias atrás"; }
        }
        public string NumeroDeMultasDescrita
        {
            get { return NumeroDeMultas + " multas"; }
        }
    }
}