using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MultasSociais.Lib.Annotations;
using Newtonsoft.Json;

namespace MultasSociais.Lib.Models
{
    public class Multa : INotifyPropertyChanged
    {
        private string descricao;
        private string placa;
        private string fotoUrl;
        private int numeroDeMultas;
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
        public int NumeroDeMultas
        {
            get { return numeroDeMultas; }
            set
            {
                numeroDeMultas = value;
                OnPropertyChanged();
                OnPropertyChanged("NumeroDeMultasDescrita");
            }
        }

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
                if (NumeroDeMultas == 1)
                {
                    return "Uma multa";
                }
                return NumeroDeMultas + " multas";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}