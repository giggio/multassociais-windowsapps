using System.Threading.Tasks;
using Caliburn.Micro;
using MultasSociais.Lib.Models;

namespace MultasSociais.WinStoreApp.ViewModels
{
    public class MultaNovaViewModel : ViewModelBase
    {
        private bool multando;
        public MultaNovaViewModel(INavigationService navigationService, ITalao talao) : base(navigationService, talao)
        {
            MontarDadosDaMulta();
        }

        private void MontarDadosDaMulta()
        {
            dadosDaMulta = new DadosDaMultaViewModel();
            dadosDaMulta.PropertyChanged += (s, e) => { if (e.PropertyName == "IsValid") NotifyOfPropertyChange("CanMultar"); };
        }

        public bool CanMultar { get { return dadosDaMulta.IsValid && !multando; } }

        public async Task Multar()
        {
            Multando = true;
            await Task.Delay(5000);
        }

        public void Cancelar()
        {
            GoBack();
        }

        public bool Multando
        {
            get { return multando; }
            set
            {
                multando = value;
                dadosDaMulta.IsEnabled = !multando;
                NotifyOfPropertyChange();
            }
        }

        private DadosDaMultaViewModel dadosDaMulta;
        public DadosDaMultaViewModel DadosDaMulta { get { return dadosDaMulta; } set { dadosDaMulta = value; NotifyOfPropertyChange(); } }
    }
}
