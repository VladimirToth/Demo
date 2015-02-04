using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using Demo.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Splashscreen : Page
    {
        internal Rect splashImageRect; // Rect to store splash screen image coordinates.
        private SplashScreen splash; // Variable to hold the splash screen object.
        internal bool dismissed = false; // Variable to track splash screen dismissal status.
        internal Frame rootFrame;

        public Splashscreen(SplashScreen splashscreen, bool loadState)
{
    InitializeComponent();

    // Listen for window resize events to reposition the extended splash screen image accordingly.
    // This ensures that the extended splash screen formats properly in response to window resizing.
    Window.Current.SizeChanged += new WindowSizeChangedEventHandler(Splashscreen_OnResize);

    splash = splashscreen;
    if (splash != null)
    {
        // Register an event handler to be executed when the splash screen has been dismissed.
        splash.Dismissed += new TypedEventHandler<SplashScreen, Object>(DismissedEventHandler);

        // Retrieve the window coordinates of the splash screen image.
        splashImageRect = splash.ImageLocation;
        PositionImage();

        // If applicable, include a method for positioning a progress control.
        PositionRing();
    }

    // Create a Frame to act as the navigation context 
    rootFrame = new Frame();
            
    // Restore the saved session state if necessary
    RestoreStateAsync(loadState);
}

        void PositionImage()
        {
            splashImage.SetValue(Canvas.LeftProperty, splashImageRect.X);
            splashImage.SetValue(Canvas.TopProperty, splashImageRect.Y);
            splashImage.Height = splashImageRect.Height;
            splashImage.Width = splashImageRect.Width;
        }

        void PositionRing()
        {
            splashProgressRing.SetValue(Canvas.LeftProperty, splashImageRect.X + (splashImageRect.Width * 0.5) - (splashProgressRing.Width * 0.5));
            splashProgressRing.SetValue(Canvas.TopProperty, (splashImageRect.Y + splashImageRect.Height + splashImageRect.Height * 0.1));
        }

        void DismissExtendedSplash()
        {
            // Navigate to mainpage
            rootFrame.Navigate(typeof(MainPage));
            // Place the frame in the current Window
            Window.Current.Content = rootFrame;
            
        }

        void Splashscreen_OnResize(Object sender, WindowSizeChangedEventArgs e)
        {
            // Safely update the extended splash screen image coordinates. This function will be executed when a user resizes the window.
            if (splash != null)
            {
                // Update the coordinates of the splash screen image.
                splashImageRect = splash.ImageLocation;
                PositionImage();

                // If applicable, include a method for positioning a progress control.
                // PositionRing();
            }
        }

        async void RestoreStateAsync(bool loadState)
        {
            if (loadState)
                await SuspensionManager.RestoreAsync();
        }

        void DismissedEventHandler(SplashScreen sender, object e)
        {
            dismissed = true;

        }

        void DismissSplashButton_Click(object sender, RoutedEventArgs e)
        {
            DismissExtendedSplash();
        }
        //public Splashscreen()
        //{
         //   this.InitializeComponent();
       // }
    }
}
