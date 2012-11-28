using System.Collections.ObjectModel;
using Caliburn.Micro;
using MultasSociais.WinStoreApp.DataModel;
using MultasSociais.WinStoreApp.Models;

namespace MultasSociais.WinStoreApp.ViewModels
{
    public class ItemDetailViewModel : ViewModelBase
    {

        private SampleDataItem selectedItem;
        private SampleDataGroup @group;
        private ObservableCollection<SampleDataItem> items;

        public ItemDetailViewModel(INavigationService navigationService, ITalao talao) : base(navigationService, talao) {}
        
        protected override void BeforeInitialize()
        {
            if (selectedItem != null && selectedItem.UniqueId == Parameter)
                return;
            var item = SampleDataSource.GetItem(Parameter);
            SelectedItem = item;
            Group = item.Group;
            Items = Group.Items;
            base.BeforeInitialize();
        }
        public SampleDataGroup Group
        {
            get
            {
                return @group;
            }
            set 
            { 
                @group = value;
                NotifyOfPropertyChange();
            }
        }

        public ObservableCollection<SampleDataItem> Items
        {
            get
            {
                return items;
            }
            set { 
                items = value;
                NotifyOfPropertyChange();
            }
        }

        public SampleDataItem SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                NotifyOfPropertyChange();
            }
        }        
    }
}
