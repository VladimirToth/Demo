﻿using Demo.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using Windows.Web.Http;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Demo
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();


        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public MainPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
            
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
          
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            //e.PageState["greetingOutputText"] = greetingOutput.Text;
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
            PopulateListbox1();
            listBox2.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            listView1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            price.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            blockPrice.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            button1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion


        private void PopulateListbox1()
        {

            var rootObject = (App.Current as App).RootObject;
            Data.numberOfStations = rootObject.stations.Count();

            for (int i = 0; i < Data.numberOfStations - 1; i++)
            {
                listBox1.Items.Add(rootObject.stations[i]);
            }  
        }
 

        private void PayTicket(object sender, RoutedEventArgs e)
        {
            if (Data.selectedStation2!=null)
            {
                this.Frame.Navigate(typeof(PayTicket));
            }

        }

        private void pageTitle_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (Data.selectedStation2 != null)
            {
                this.Frame.Navigate(typeof(PayTicket));
            }
        }

        
        private void nameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listBox2.Visibility = Windows.UI.Xaml.Visibility.Visible;

            ListBox lb = (ListBox)sender;
            Data.selectedStation1 = lb.SelectedItem as Station;

            listBox2.Items.Clear();
            price.Text = "";

            if (Data.selectedStation1 != null)
            {
                var rootObject = (App.Current as App).RootObject;
                int index = rootObject.stations.IndexOf(Data.selectedStation1);
                for (int i = index + 1; i < Data.numberOfStations; i++)
                {
                    listBox2.Items.Add(rootObject.stations[i]);
                }
            }
        }

        private void listBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listView1.Visibility = Windows.UI.Xaml.Visibility.Visible;
            price.Visibility = Windows.UI.Xaml.Visibility.Visible;
            blockPrice.Visibility = Windows.UI.Xaml.Visibility.Visible;
            button1.Visibility = Windows.UI.Xaml.Visibility.Visible;

            ListBox lb = (ListBox)sender;
            Data.selectedStation2 = lb.SelectedItem as Station;

            listView1.Items.Clear();
            if (Data.selectedStation2 != null)
            {
                var rootObject = (App.Current as App).RootObject;
                int start = rootObject.stations.IndexOf(Data.selectedStation1);
                int end = rootObject.stations.IndexOf(Data.selectedStation2);

                for (int i = start; i <= end; i++)
                {
                    rootObject.stations[i].tempDistance = rootObject.stations[i].distance - Data.selectedStation1.distance;
                    rootObject.stations[i].tempDuration = TimeSpan.FromMinutes(rootObject.stations[i].duration - Data.selectedStation1.duration);

                    listView1.Items.Add(rootObject.stations[i]);
                }

                priceCalculation(rootObject.stations[end].tempDistance);
            }            
        }

        private void priceCalculation(int distance)
        {
            Data.price = distance * 5;
            price.Text = Data.price.ToString() + " YUANS";
        }
    }
}
