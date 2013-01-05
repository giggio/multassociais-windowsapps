using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Navigation;
using Caliburn.Micro;
using Microsoft.Phone.Controls;
using MultasSociais.Lib.Models;
using MultasSociais.WinPhone8App.Infra;
using MultasSociais.WinPhone8App.Models;
using MultasSociais.WinPhone8App.ViewModels;

namespace MultasSociais.WinPhone8App
{
    public class AppBootstrapper : PhoneBootstrapper
    {
        PhoneContainer container;
        private bool wasRelaunched;

        protected override void Configure()
        {
            container = new PhoneContainer(RootFrame);

            container.RegisterPhoneServices();
            container.PerRequest<MainViewModel>(); 
            container.PerRequest<DetailsViewModel>();
            container.PerRequest<ITalao, Talao>();
            container.RegisterSingleton(typeof(IMultasRealizadas), null, typeof(MultasRealizadas));
            container.Instance<IObjectStorageHelper<ListaDeMultasRealizadas>>(new ObjectStorageHelper<ListaDeMultasRealizadas>());
            AddCustomConventions();
#if DEBUG
            //LogManager.GetLog = type => new DebugLogger(type);
#endif
            MapeiaRootFrame();
        }

        private void MapeiaRootFrame()
        {
            RootFrame.Navigating += Navigating;
            RootFrame.UriMapper = new Mapeador();
        }

        private void Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Reset)
            {
                // This block will execute if the current navigation is a relaunch.
                // If so, another navigation will be coming, so this records that a relaunch just happened
                // so that the next navigation can use this info.
                wasRelaunched = true;
            }
            else if (e.NavigationMode == NavigationMode.New && wasRelaunched)
            {
                // This block will run if the previous navigation was a relaunch
                wasRelaunched = false;
                if (!IsShare(e.Uri))
                    e.Cancel = true;
            }
        }

        private bool IsShare(Uri uri)
        {
            var stringUri = uri.ToString();
            return ((stringUri.Contains("ShareContent")) && (stringUri.Contains("FileId")));
        }

        static void AddCustomConventions()
        {
            ConventionManager.AddElementConvention<Pivot>(Pivot.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
                (viewModelType, path, property, element, convention) =>
                {
                    if (ConventionManager
                        .GetElementConvention(typeof(ItemsControl))
                        .ApplyBinding(viewModelType, path, property, element, convention))
                    {
                        ConventionManager.ConfigureSelectedItem(element, Pivot.SelectedItemProperty, viewModelType, path);
                        ConventionManager.ApplyHeaderTemplate(element, Pivot.HeaderTemplateProperty, null, viewModelType);
                        return true;
                    }

                    return false;
                };

            ConventionManager.AddElementConvention<Panorama>(Panorama.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
                (viewModelType, path, property, element, convention) =>
                {
                    if (ConventionManager
                        .GetElementConvention(typeof(ItemsControl))
                        .ApplyBinding(viewModelType, path, property, element, convention))
                    {
                        ConventionManager.ConfigureSelectedItem(element, Panorama.SelectedItemProperty, viewModelType, path);
                        ConventionManager.ApplyHeaderTemplate(element, Panorama.HeaderTemplateProperty, null, viewModelType);
                        return true;
                    }
                    return false;
                };
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
    }
}