using System.Collections.Generic;
using Caliburn.Micro;
using MultasSociais.Lib.Models;

namespace MultasSociais.WinStoreApp.ViewModels
{
    public class GroupDetailViewModel : ViewModelBase<GrupoDeMultas>
    {
        public GroupDetailViewModel(INavigationService navigationService, ITalao talao) : base(navigationService, talao){}

        protected override void OnInitialize()
        {
            Grupo = Parameter;
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