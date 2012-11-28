using Caliburn.Micro;
using System;
using System.Collections.Generic;
using MultasSociais.WinStoreApp.Models;
using MultasSociais.WinStoreApp.Views;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;

namespace MultasSociais.WinStoreApp
{
    sealed partial class App
    {
        private WinRTContainer container;

        public App()
        {
            InitializeComponent();
        }

        protected override void Configure()
        {
            container = new WinRTContainer();
            container.RegisterWinRTServices();
            container.PerRequest<ITalao, Talao>();
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
            DisplayRootView<GroupedItemsView>("AllGroups");
        }

        protected override void OnShareTargetActivated(ShareTargetActivatedEventArgs args)
        {
            //todo: share
        }
    }
}
