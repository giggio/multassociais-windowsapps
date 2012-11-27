using System.Collections.ObjectModel;
using Caliburn.Micro;
using MultasSociais.WinStoreApp.DataModel;

namespace MultasSociais.WinStoreApp.ViewModels
{
    public class ItemDetailViewModel : ViewModelBase
    {
        public ItemDetailViewModel(INavigationService navigationService) : base(navigationService) {}
        protected override void OnInitialize()
        {
            var item = SampleDataSource.GetItem(Parameter);
            if (item != selectedItem) SelectedItem = item;
            base.OnInitialize();
        }
        public string Parameter { get; set; }

        public SampleDataGroup Group
        {
            get
            {
                return selectedItem == null ? null : selectedItem.Group;
            }
        }

        public ObservableCollection<SampleDataItem> Items
        {
            get
            {
                return selectedItem == null ? null : selectedItem.Group.Items;
            }
        }

        private SampleDataItem selectedItem;
        public SampleDataItem SelectedItem
        {
            get
            {
                if (selectedItem == null)
                {
                    var item = SampleDataSource.GetItem(Parameter);
                    SelectedItem = item;
                }
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                NotifyOfPropertyChange("Group");
                NotifyOfPropertyChange("Items");
                NotifyOfPropertyChange();
            }
        }        

    }
}
