using System.Collections.Generic;
using Caliburn.Micro;
using MultasSociais.Lib.Models;

namespace MultasSociais.WinStoreApp.ViewModels
{
    public class GroupedItemsViewModel : ViewModelBase<string>
    {
        public GroupedItemsViewModel(INavigationService navigationService, ITalao talao) : base(navigationService, talao) {}

        protected async override void OnInitialize()
        {
            var grupos = new BindableCollection<GrupoDeMultas>{await talao.ObterMaisNovos(), await talao.ObterMaisMultados()};
            ConfigurarNumeroDeItensAExibir(grupos);
            Grupos = grupos;
            base.OnInitialize();
        }

        private void ConfigurarNumeroDeItensAExibir(IEnumerable<GrupoDeMultas> grupos)
        {
            var top = 6;
            foreach (var grupo in grupos)
            {
                grupo.TopCount = top;
            }
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