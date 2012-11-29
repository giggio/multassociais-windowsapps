using Caliburn.Micro;
using MultasSociais.Lib.Models;
using MultasSociais.WinStoreApp.Views;

namespace MultasSociais.WinStoreApp.ViewModels
{
    public abstract class ViewModelBase : Screen
    {
        protected readonly INavigationService navigationService;
        protected readonly ITalao talao;

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

        protected virtual void BeforeInitialize()
        {
        }

        public void GoToHeader(GrupoDeMultas grupoDeMultas)
        {
            navigationService.Navigate<GroupDetailView>(grupoDeMultas.Nome);
        }
        public void GoToItem(Multa multa)
        {
            navigationService.Navigate<ItemDetailView>(multa);
        }
    }

    public abstract class ViewModelBase<T> : ViewModelBase
    {
        protected ViewModelBase(INavigationService navigationService, ITalao talao) : base(navigationService, talao) {}

        private T parameter;
        public T Parameter
        {
            get { return parameter; }
            set
            {
                parameter = value;
                BeforeInitialize();
            }
        }
    }
}