using Caliburn.Micro;
using System;
using System.Collections.Generic;
using MultasSociais.Lib.Models;
using MultasSociais.WinStoreApp.Models;
using MultasSociais.WinStoreApp.ViewModels;
using MultasSociais.WinStoreApp.Views;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
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
            DisplayRootView<GroupedItemsView>("AllGroups");
        }

        protected override void OnShareTargetActivated(ShareTargetActivatedEventArgs args)
        {
            ShareTargetViewModel.ShareOperation = args.ShareOperation; 
            DisplayRootViewFor<ShareTargetViewModel>();
        }
    }
}
