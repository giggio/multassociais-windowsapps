using System;

namespace MultasSociais.Lib.Models
{
    public class Multa
    {
        public int Id { get; set; }
        public DateTime DataOcorrencia { get; set; }
        public string Descricao { get; set; }
        public string Placa { get; set; }
        public int NumeroDeMultas { get; set; }
        public string VideoUrl { get; set; }
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