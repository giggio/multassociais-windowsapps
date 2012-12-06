using System.Threading.Tasks;
using Caliburn.Micro;
using MultasSociais.Lib.Models;

namespace MultasSociais.WinStoreApp.ViewModels
{
    public class MultaNovaViewModel : ViewModelBase
    {
        private bool multando;
        public MultaNovaViewModel(INavigationService navigationService, ITalao talao) : base(navigationService, talao) {}

        public async Task Multar()
        {
            Multando = true;
            await Task.Delay(5000);
        }

        public bool Multando
        {
            get { return multando; }
            set
            {
                multando = value;
                NotifyOfPropertyChange();
            }
        }
    }
}
