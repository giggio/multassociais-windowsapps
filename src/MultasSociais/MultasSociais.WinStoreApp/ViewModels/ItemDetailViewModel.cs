using System.Collections.Generic;
using System.Threading.Tasks;
using Caliburn.Micro;
using MultasSociais.Lib.Models;

namespace MultasSociais.WinStoreApp.ViewModels
{
    public class ItemDetailViewModel : ViewModelBase<Multa>
    {

        private Multa selectedItem;
        private GrupoDeMultas grupo;
        private IEnumerable<Multa> itens;
        private bool possivelMultar = true;

        public ItemDetailViewModel(INavigationService navigationService, ITalao talao) : base(navigationService, talao) {}
        
        protected override void BeforeInitialize()
        {
            if (selectedItem != null && selectedItem == Parameter)
                return;
            var multa = Parameter;
            SelectedItem = multa;
            Grupo = multa.Grupo;
            Itens = Grupo.Itens;
            base.BeforeInitialize();
        }

        public async Task Multar()
        {
            var multadoComSucesso = await talao.MarcarMultaAsync(selectedItem);
            if (multadoComSucesso)
            {
                selectedItem.NumeroDeMultas++;
                CanMultar = false;
            }
        }

        public bool CanMultar
        {
            get { return possivelMultar; }
            set 
            { 
                possivelMultar = value;
                NotifyOfPropertyChange();
            }
        }
        
        public GrupoDeMultas Grupo
        {
            get
            {
                return grupo;
            }
            set 
            { 
                grupo = value;
                NotifyOfPropertyChange();
            }
        }

        public IEnumerable<Multa> Itens
        {
            get
            {
                return itens;
            }
            set { 
                itens = value;
                NotifyOfPropertyChange();
            }
        }

        public Multa SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange("CanMultar");
            }
        }        
    }
}