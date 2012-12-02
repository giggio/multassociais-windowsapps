using System.Collections.Generic;
using System.Threading.Tasks;
using Caliburn.Micro;
using MultasSociais.Lib.Models;
using MultasSociais.WinStoreApp.Models;

namespace MultasSociais.WinStoreApp.ViewModels
{
    public class ItemDetailViewModel : ViewModelBase<Multa>
    {
        private readonly IMultasRealizadas multasRealizadas;

        private Multa selectedItem;
        private GrupoDeMultas grupo;
        private IEnumerable<Multa> itens;
        private bool multadoAgora = true;
        public ItemDetailViewModel(INavigationService navigationService, ITalao talao, IMultasRealizadas multasRealizadas) : base(navigationService, talao)
        {
            this.multasRealizadas = multasRealizadas;
        }

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
            multadoAgora = await talao.MarcarMultaAsync(selectedItem);
            if (multadoAgora)
            {
                NotifyOfPropertyChange("CanMultar");
                selectedItem.NumeroDeMultas++;
                await GuardarQueFoiMultado();
            }
        }
        private async Task GuardarQueFoiMultado()
        {
            await multasRealizadas.Adicionar(new MultaRealizada{Id = selectedItem.Id});
        }

        public bool CanMultar
        {
            get { return !multadoAgora && !multasRealizadas.FoiMultado(selectedItem); }
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
                multadoAgora = false;
                selectedItem = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange("CanMultar");
            }
        }        
    }
}