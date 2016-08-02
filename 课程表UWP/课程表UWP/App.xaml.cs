using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace 课程表UWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
    
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            bool isHardwareButtonsAPIPresent = Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons");
            if (isHardwareButtonsAPIPresent)
            {
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            }
        }

        //按后退键后退
        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null && rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
                e.Handled = true;
            }
        }


        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {

            //隐藏手机状态栏！
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent(typeof(StatusBar).ToString()))
            {
                StatusBar StatusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
                await StatusBar.HideAsync();
            }


            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
                rootFrame.Navigated += Show_Navigation;
                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// App_BackRequested
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            // 这里面可以任意选择控制哪个Frame 
            // 如果MainPage.xaml中使用了另外的Frame标签进行导航 可在此处获取需要GoBack的Frame
            var rootFrame = Window.Current.Content as Frame;
            // ReSharper disable once PossibleNullReferenceException
            if (!rootFrame.CanGoBack) return;
            rootFrame.GoBack();
            // 设置指示应用程序已执行请求的后退导航操作
            e.Handled = true;
        }

        /// <summary>
        /// Show_Navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Show_Navigation(object sender, NavigationEventArgs e)
        {
            // 每次完成导航 确定下是否显示系统后退按钮
            // ReSharper disable once PossibleNullReferenceException
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
               (Window.Current.Content as Frame).BackStack.Any()
               ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
