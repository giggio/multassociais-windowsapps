using System;
using Newtonsoft.Json;

namespace MultasSociais.Lib.Models
{
    public class Multa
    {
        private string descricao;
        private string placa;
        private string fotoUrl;
        public int Id { get; set; }
        [JsonProperty(PropertyName = "data_ocorrencia")]
        public DateTime DataOcorrencia { get; set; }
        public string Descricao
        {
            get
            {
                return string.IsNullOrWhiteSpace(descricao) ? "Nenhuma descri��o informada" : descricao;
            }
            set { descricao = value; }
        }

        public string Placa
        {
            get
            {
                return string.IsNullOrWhiteSpace(placa) ? "n�o informada" : placa;
            }
            set { placa = value; }
        }

        [JsonProperty(PropertyName = "likes")]
        public int NumeroDeMultas { get; set; }
        [JsonProperty(PropertyName = "video")]
        public string VideoUrl { get; set; }
        [JsonProperty(PropertyName = "foto_url")]
        public string FotoUrl
        {
            get
            {
                return string.IsNullOrWhiteSpace(fotoUrl) ? "ms-appx:///Assets/SemImagem.png" : fotoUrl;
            }
            set { fotoUrl = value; }
        }

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
                if (diasDaOcorrencia == 1)
                {
                    return "Ontem";
                }
                return diasDaOcorrencia + " dias atr�s";
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
                if (NumeroDeMultas == 1)
                {
                    return "Uma multa";
                }
                return NumeroDeMultas + " multas";
            }
        }
    }
}