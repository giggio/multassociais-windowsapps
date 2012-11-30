using System.Threading.Tasks;
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
                ConstruirItens();
                NotifyOfPropertyChange();
                NotifyOfPropertyChange("Itens");
            }
        }

        private void ConstruirItens()
        {
            itens = new ListaVirtualizada<Multa>(grupo.Itens, numeroAObter => talao.PegarMaisMultas(grupo.TipoGrupo, grupo.Itens.Count, numeroAObter));
        }

        private ListaVirtualizada<Multa> itens;
        public ListaVirtualizada<Multa> Itens
        {
            get
            {
                return itens;
            }
        }
    }
}