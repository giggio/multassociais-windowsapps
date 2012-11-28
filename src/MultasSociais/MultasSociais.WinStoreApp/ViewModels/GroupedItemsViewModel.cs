using Caliburn.Micro;
using MultasSociais.WinStoreApp.DataModel;

namespace MultasSociais.WinStoreApp.ViewModels
{
    public class GroupedItemsViewModel : ViewModelBase
    {
        public GroupedItemsViewModel(INavigationService navigationService) : base(navigationService) {}

        protected override void OnInitialize()
        {
            Groups = new BindableCollection<SampleDataGroup>(SampleDataSource.GetGroups(Parameter));
            base.OnInitialize();
        }

        private BindableCollection<SampleDataGroup> groups;
        public BindableCollection<SampleDataGroup> Groups
        {
            set
            {
                groups = value;
                NotifyOfPropertyChange();
            }
            get
            {
                return groups;
            }
        }

    }
}
