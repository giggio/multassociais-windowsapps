using System.Collections.Generic;
using Caliburn.Micro;
using MultasSociais.Lib.Models;

namespace MultasSociais.WinPhone8App.ViewModels
{
    public class MainViewModel : BaseScreen
    {
        readonly INavigationService navigationService;
        private readonly ITalao talao;

        public MainViewModel(INavigationService navigationService, ITalao talao)
        {
            this.navigationService = navigationService;
            this.talao = talao;
        }

        protected async override void OnInitialize()
        {
            var grupos = new BindableCollection<GrupoDeMultas> { await talao.ObterMaisNovos(), await talao.ObterMaisMultados() };
            ConfigurarNumeroDeItensAExibir(grupos);
            Grupos = grupos;
            IsLoading = false;
            base.OnInitialize();
        }

        private void ConfigurarNumeroDeItensAExibir(IEnumerable<GrupoDeMultas> grupos)
        {
            var top = 10;
            foreach (var grupo in grupos)
            {
                grupo.TopCount = top;
            }
        }

        private bool isLoading = true;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                NotifyOfPropertyChange();
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

        public void GoToItem(ItemViewModel item)
        {
            navigationService.UriFor<DetailsViewModel>()
                .WithParam(x => x.ItemId, item.ID)
                .Navigate();
        }
    }
}
