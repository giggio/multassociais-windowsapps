using Caliburn.Micro;
using System;
using System.Collections.Generic;
using MultasSociais.Lib.Models;
using MultasSociais.WinStoreApp.Models;
using MultasSociais.WinStoreApp.ViewModels;
using MultasSociais.WinStoreApp.Views;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml.Controls;

namespace MultasSociais.WinStoreApp
{
    sealed partial class App
    {
        private WinRTContainer container;

        public App()
        {
            InitializeComponent();
            MultasRealizadas.Inicializar();
        }

        protected override void Configure()
        {
            container = new WinRTContainer();
            container.RegisterWinRTServices();
            container.PerRequest<ITalao, Talao>();
            container.PerRequest<IMultasRealizadas, MultasRealizadas>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            container.RegisterNavigationService(rootFrame);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (args.Kind == ActivationKind.ShareTarget) return;
            ConfigurarSettings();
            if (ExibirPrimeiraRodada())
                DisplayRootView<PrimeiraRodadaView>();
            else
                DisplayRootView<GroupedItemsView>("AllGroups");
        }

        private static bool ExibirPrimeiraRodada()
        {
            var configuracoes = ApplicationData.Current.LocalSettings.Values;
            return !configuracoes.ContainsKey("termosDeUsoAceitos");
        }

        private void ConfigurarSettings()
        {
            SettingsPane.GetForCurrentView().CommandsRequested +=
                (sender, args) =>
                    {
                        
                        var generalCommand = new SettingsCommand("politicaDePrivacidade", "Política de privacidade", comando => Windows.System.Launcher.LaunchUriAsync(new Uri("https://github.com/giggio/multassociais-windowsapps/blob/master/README.md")));
                        args.Request.ApplicationCommands.Add(generalCommand);
                    };
        }

        protected override void OnShareTargetActivated(ShareTargetActivatedEventArgs args)
        {
            ShareTargetViewModel.ShareOperation = args.ShareOperation; 
            DisplayRootViewFor<ShareTargetViewModel>();
        }
    }
}
