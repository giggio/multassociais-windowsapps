using Caliburn.Micro;
using MultasSociais.Lib.Models;

namespace MultasSociais.WinStoreApp.ViewModels
{
    public class GroupedItemsViewModel : ViewModelBase<string>
    {
        public GroupedItemsViewModel(INavigationService navigationService, ITalao talao) : base(navigationService, talao) {}

        protected async override void OnInitialize()
        {
            Grupos = new BindableCollection<GrupoDeMultas>{await talao.ObterMaisNovos(), await talao.ObterMaisMultados()};
            base.OnInitialize();
        }

        private BindableCollection<GrupoDeMultas> grupos;
        public BindableCollection<GrupoDeMultas> Grupos
        {
            set
            {
                grupos = value;
                NotifyOfPropertyChange();
            }
            get
            {
                return grupos;
            }
        }

    }
}