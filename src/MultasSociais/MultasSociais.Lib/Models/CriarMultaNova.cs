using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace MultasSociais.Lib.Models
{
    public partial class CriarMultaNova
    {
        public void SetaDataOcorrencia(DateTime dataOcorrencia)
        {
            DataOcorrenciaAno = dataOcorrencia.Year.ToString();
            DataOcorrenciaMes = dataOcorrencia.Month.ToString();
            DataOcorrenciaDia = dataOcorrencia.Day.ToString();
            DataOcorrenciaHora = dataOcorrencia.Hour.ToString();
            DataOcorrenciaMinuto = dataOcorrencia.Minute.ToString();
        }
        [JsonProperty(PropertyName = "api_id")]
        public string ApiId { get { return apiId; } }
        [JsonProperty(PropertyName = "api_secret")]
        public string ApiSecret { get { return apiSecret; } }
        [JsonProperty(PropertyName = "multa[data_ocorrencia(1i)]")]
        public string DataOcorrenciaAno { get; private set; }
        [JsonProperty(PropertyName = "multa[data_ocorrencia(2i)]")]
        public string DataOcorrenciaMes { get; private set; }
        [JsonProperty(PropertyName = "multa[data_ocorrencia(3i)]")]
        public string DataOcorrenciaDia { get; private set; }
        [JsonProperty(PropertyName = "multa[data_ocorrencia(4i)]")]
        public string DataOcorrenciaHora { get; private set; }
        [JsonProperty(PropertyName = "multa[data_ocorrencia(5i)]")]
        public string DataOcorrenciaMinuto { get; private set; }
        [JsonProperty(PropertyName = "multa[descricao]")]
        public string Descricao { get; set; }
        [JsonProperty(PropertyName = "multa[placa]")]
        public string Placa { get; set; }
        [JsonProperty(PropertyName = "multa[video]")]
        public string VideoUrl { get; set; }

        public Dictionary<string, string> ObterValores()
        {
            var valores = new Dictionary<string, string>();
            var props = typeof (CriarMultaNova).GetTypeInfo().DeclaredProperties;
            foreach (var prop in props)
            {
                var attr = prop.GetCustomAttribute<JsonPropertyAttribute>();
                var name = attr == null ? prop.Name : attr.PropertyName;
                var valorObjeto = prop.GetValue(this, null);
                if (valorObjeto == null) continue;
                var valorString = valorObjeto.ToString();
                if (!string.IsNullOrWhiteSpace(valorString))
                {
                    valores.Add(name, valorString);
                }
            }
            return valores;
        }
    }
}