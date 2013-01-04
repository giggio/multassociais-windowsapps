using Caliburn.Micro;

namespace MultasSociais.WinPhone8App.ViewModels
{
    public class DetailsViewModel : BaseScreen
    {
        private readonly INavigationService navigationService;

        public DetailsViewModel() {}

        public DetailsViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public string ItemId { get; set; }

        private ItemViewModel item;
        public ItemViewModel Item
        {
            get { return item; }
            set
            {
                if (Equals(value, item)) return;
                item = value;
                NotifyOfPropertyChange();
            }
        }

        protected override void OnActivate()
        {
            Item = new ItemViewModel {ID = "0", LineOne = "runtime one", LineTwo = "Maecenas praesent accumsan bibendum", LineThree = "Facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu"};
            base.OnActivate();
        }
    }
}
