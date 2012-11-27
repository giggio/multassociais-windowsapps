using Caliburn.Micro;
using MultasSociais.WinStoreApp.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MultasSociais.WinStoreApp.Views;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Grid App template is documented at http://go.microsoft.com/fwlink/?LinkId=234226

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
        //public App()
        //{
        //    this.InitializeComponent();
        //    this.Suspending += OnSuspending;
        //}

        ///// <summary>
        ///// Invoked when the application is launched normally by the end user.  Other entry points
        ///// will be used when the application is launched to open a specific file, to display
        ///// search results, and so forth.
        ///// </summary>
        ///// <param name="args">Details about the launch request and process.</param>
        //protected override async void OnLaunched(LaunchActivatedEventArgs args)
        //{
        //    var rootFrame = Window.Current.Content as Frame;

        //    // Do not repeat app initialization when the Window already has content,
        //    // just ensure that the window is active
            
        //    if (rootFrame == null)
        //    {
        //        // Create a Frame to act as the navigation context and navigate to the first page
        //        rootFrame = new Frame();
        //        //Associate the frame with a SuspensionManager key                                
        //        SuspensionManager.RegisterFrame(rootFrame, "AppFrame");

        //        if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
        //        {
        //            // Restore the saved session state only when appropriate
        //            try
        //            {
        //                await SuspensionManager.RestoreAsync();
        //            }
        //            catch (SuspensionManagerException)
        //            {
        //                //Something went wrong restoring state.
        //                //Assume there is no state and continue
        //            }
        //        }

        //        // Place the frame in the current Window
        //        Window.Current.Content = rootFrame;
        //    }
        //    if (rootFrame.Content == null)
        //    {
        //        // When the navigation stack isn't restored navigate to the first page,
        //        // configuring the new page by passing required information as a navigation
        //        // parameter
        //        if (!rootFrame.Navigate(typeof(GroupedItemsView), "AllGroups"))
        //        {
        //            throw new Exception("Failed to create initial page");
        //        }
        //    }
        //    // Ensure the current window is active
        //    Window.Current.Activate();
        //}

        ///// <summary>
        ///// Invoked when application execution is being suspended.  Application state is saved
        ///// without knowing whether the application will be terminated or resumed with the contents
        ///// of memory still intact.
        ///// </summary>
        ///// <param name="sender">The source of the suspend request.</param>
        ///// <param name="e">Details about the suspend request.</param>
        //private async void OnSuspending(object sender, SuspendingEventArgs e)
        //{
        //    var deferral = e.SuspendingOperation.GetDeferral();
        //    await SuspensionManager.SaveAsync();
        //    deferral.Complete();
        //}
    }
}
