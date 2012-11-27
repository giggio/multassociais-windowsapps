using System.Collections.ObjectModel;
using Caliburn.Micro;
using MultasSociais.WinStoreApp.DataModel;
using MultasSociais.WinStoreApp.Views;

namespace MultasSociais.WinStoreApp.ViewModels
{
    public class GroupDetailViewModel : ViewModelBase
    {
        public GroupDetailViewModel(INavigationService navigationService) : base(navigationService){}

        protected override void OnInitialize()
        {
            Group = SampleDataSource.GetGroup(Parameter);
            base.OnInitialize();
        }

        private SampleDataGroup @group;
        public SampleDataGroup Group
        {
            get { return @group; }
            set
            {
                @group = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange("Items");
            }
        }
        public ObservableCollection<SampleDataItem> Items
        {
            get
            {
                return @group == null ? null : @group.Items;
            }
        }
        public void GoToItem(SampleDataItem sampleDataItem)
        {
            navigationService.Navigate<ItemDetailView>(sampleDataItem.UniqueId);
        }
    }
}