using System;
using Newtonsoft.Json;

namespace MultasSociais.Lib.Models
{
    public class Multa
    {
        private string descricao;
        private string placa;
        public int Id { get; set; }
        [JsonProperty(PropertyName = "data_ocorrencia")]
        public DateTime DataOcorrencia { get; set; }
        public string Descricao
        {
            get
            {
                return string.IsNullOrWhiteSpace(descricao) ? "Nenhuma descrição informada" : descricao;
            }
            set { descricao = value; }
        }

        public string Placa
        {
            get
            {
                return string.IsNullOrWhiteSpace(placa) ? "não informada" : placa;
            }
            set { placa = value; }
        }

        [JsonProperty(PropertyName = "likes")]
        public int NumeroDeMultas { get; set; }
        [JsonProperty(PropertyName = "video")]
        public string VideoUrl { get; set; }
        [JsonProperty(PropertyName = "foto_url")]
        public string FotoUrl { get; set; }
        public GrupoDeMultas Grupo { get; set; }

        public string DataDescrita
        {
            get
            {
                var diasDaOcorrencia = Math.Round(DateTime.UtcNow.Subtract(DataOcorrencia).TotalDays, 0);
                if (diasDaOcorrencia == 0)
                {
                    return "Hoje";
                }
                return diasDaOcorrencia + " dias atrás";
            }
        }
        public string NumeroDeMultasDescrita
        {
            get
            {
                if (NumeroDeMultas == 0)
                {
                    return "Nenhuma multa";
                }
                return NumeroDeMultas + " multas";
            }
        }
    }
}