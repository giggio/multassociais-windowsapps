using Caliburn.Micro;

namespace MultasSociais.WinStoreApp.ViewModels
{
    public abstract class ViewModelBase : Screen
    {
        protected readonly INavigationService navigationService;

        protected ViewModelBase(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public void GoBack()
        {
            navigationService.GoBack();
        }

        public bool CanGoBack
        {
            get
            {
                return navigationService.CanGoBack;
            }
        }
    }
}