using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using MultasSociais.Lib.Models;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace MultasSociais.WinStoreApp.ViewModels
{
    public class PrimeiraRodadaViewModel : ViewModelBase
    {
        public PrimeiraRodadaViewModel(INavigationService navigationService, ITalao talao) : base(navigationService, talao) {}
    
        public void Concordo()
        {
            var configuracoes = ApplicationData.Current.LocalSettings.Values;
            configuracoes.Add("termosDeUsoAceitos", true);
            GoHome();
        }
    }
}
