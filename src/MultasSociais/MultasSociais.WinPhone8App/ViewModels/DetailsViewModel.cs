using System;
using System.Threading.Tasks;
using MultasSociais.Lib.Models;
using MultasSociais.WinPhone8App.Models;

namespace MultasSociais.WinPhone8App.ViewModels
{
    public class DetailsViewModel : BaseScreen
    {
        public DetailsViewModel(ITalao talao, IMultasRealizadas multasRealizadas)
        {
            this.talao = talao;
            this.multasRealizadas = multasRealizadas;
        }

        private readonly ITalao talao;
        private readonly IMultasRealizadas multasRealizadas;
        public int Id { get; set; }
        public DateTime DataOcorrencia { get; set; }
        public string Descricao { get; set; }
        public string Placa { get; set; }
        private int numeroDeMultas;
        public int NumeroDeMultas
        {
            get { return numeroDeMultas; }
            set
            {
                if (value == numeroDeMultas) return;
                numeroDeMultas = value;
                NotifyOfPropertyChange();
            }
        }
        public string VideoUrl { get; set; }
        public string FotoUrl { get; set; }
        public string DataDescrita { get; set; }
        public string NumeroDeMultasDescrita { get; set; }
        
        private bool multando;
        private bool multadoAgora;

        public async Task Multar()
        {
            try
            {
                multando = true;
                NotifyOfPropertyChange("CanMultar");
                multadoAgora = await talao.MarcarMultaAsync(Id);
                if (multadoAgora)
                {
                    NumeroDeMultas++;
                    await GuardarQueFoiMultado();
                }
            }
            finally
            {
                multando = false;
            }
            NotifyOfPropertyChange("CanMultar");
        }

        private async Task GuardarQueFoiMultado()
        {
            await multasRealizadas.Adicionar(new MultaRealizada { Id = Id });
        }

        public bool CanMultar
        {
            get { return !multando && !multadoAgora && !multasRealizadas.FoiMultado(Id); }
        }
    }
}
