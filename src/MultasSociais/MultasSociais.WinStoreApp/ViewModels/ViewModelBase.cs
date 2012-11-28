using Caliburn.Micro;
using MultasSociais.WinStoreApp.Models;
using MultasSociais.WinStoreApp.Views;

namespace MultasSociais.WinStoreApp.ViewModels
{
    public abstract class ViewModelBase : Screen
    {
        protected readonly INavigationService navigationService;
        protected readonly ITalao talao;
        private string parameter;

        protected ViewModelBase(INavigationService navigationService, ITalao talao)
        {
            this.navigationService = navigationService;
            this.talao = talao;
        }

        public void GoBack()
        {
            navigationService.GoBack();
        }

        public bool CanGoBack
        {
            get
            {
                return navigationService.CanGoBack;
            }
        }

        public string Parameter
        {
            get { return parameter; }
            set
            {
                parameter = value;
                BeforeInitialize();
            }
        }

        protected virtual void BeforeInitialize()
        {
        }

        public void GoToHeader(GrupoDeMultas grupoDeMultas)
        {
            navigationService.Navigate<GroupDetailView>(grupoDeMultas.Nome);
        }
        public void GoToItem(Multa multa)
        {
            navigationService.Navigate<ItemDetailView>(multa.Id);
        }
    }
}