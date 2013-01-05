using System;
using System.Collections.Generic;
using System.Windows.Controls;
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