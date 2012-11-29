using System.Collections.Generic;
using Caliburn.Micro;
using MultasSociais.Lib.Models;

namespace MultasSociais.WinStoreApp.ViewModels
{
    public class GroupDetailViewModel : ViewModelBase<string>
    {
        public GroupDetailViewModel(INavigationService navigationService, ITalao talao) : base(navigationService, talao){}

        protected async override void OnInitialize()
        {
            switch (Parameter.ToLower())
            {
                case "mais multados":
                    Grupo = await talao.ObterMaisMultados();
                    break;
                case "mais novos":
                    Grupo = await talao.ObterMaisNovos();
                    break;
            }
            base.OnInitialize();
        }

        private GrupoDeMultas grupo;
        public GrupoDeMultas Grupo
        {
            get { return grupo; }
            set
            {
                grupo = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange("Itens");
            }
        }
        public IEnumerable<Multa> Itens
        {
            get
            {
                return grupo == null ? null : grupo.Itens;
            }
        }
    }
}