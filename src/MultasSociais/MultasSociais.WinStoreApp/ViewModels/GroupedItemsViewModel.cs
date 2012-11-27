using System.Collections.Generic;
using Caliburn.Micro;
using MultasSociais.WinStoreApp.DataModel;
using MultasSociais.WinStoreApp.Views;

namespace MultasSociais.WinStoreApp.ViewModels
{
    public class GroupedItemsViewModel : ViewModelBase
    {
        public GroupedItemsViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        protected override void OnInitialize()
        {
            Groups = new BindableCollection<SampleDataGroup>(SampleDataSource.GetGroups(Parameter));
            base.OnInitialize();
        }
        private string parameter;
        private BindableCollection<SampleDataGroup> groups;

        public string Parameter
        {
            get
            {
                return parameter;
            }
            set
            {
                parameter = value;
                NotifyOfPropertyChange();
            }
        }

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

        public void GoToHeader(SampleDataGroup sampleDataGroup)
        {
            navigationService.Navigate<GroupDetailView>(sampleDataGroup.UniqueId);
        }
    }
}
