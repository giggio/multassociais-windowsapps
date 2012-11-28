using Caliburn.Micro;
using MultasSociais.WinStoreApp.Models;

namespace MultasSociais.WinStoreApp.ViewModels
{
    public class GroupedItemsViewModel : ViewModelBase<string>
    {
        public GroupedItemsViewModel(INavigationService navigationService, ITalao talao) : base(navigationService, talao) {}

        protected override void OnInitialize()
        {
            Grupos = new BindableCollection<GrupoDeMultas>{talao.ObterMaisNovos(), talao.ObterMaisMultados()};
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