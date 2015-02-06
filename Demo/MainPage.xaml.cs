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
//using System.Net;
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
        private Rootobject _Rootobject;
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
            this.GetData();
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
            e.PageState["greetingOutputText"] = greetingOutput.Text;
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
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion


        //private async Task<string> Method()
        //{
        //    HttpClient client = new HttpClient();
        //    string json = await client.GetStringAsync("https://raw.githubusercontent.com/VladimirToth/Demo/master/Demo/stations.json");

        //    return json;
        //}


        private async void GetData()
        {

            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(new Uri("https://raw.githubusercontent.com/VladimirToth/Demo/master/Demo/stations.json"));
            var jsonString = await response.Content.ReadAsStringAsync();

            _Rootobject = JsonConvert.DeserializeObject<Rootobject>(jsonString);
            
            Data.numberOfStations = _Rootobject.stations.Count();

            for (int i = 0; i < Data.numberOfStations - 1; i++)
            {
                listBox1.Items.Add(_Rootobject.stations[i].name);
            }
        }

        

        private void PayTicket(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PayTicket));

        }

        private void combo1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void pageTitle_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {

        }


        private void nameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Ukladam data zapisane do TextBoxu, takze v pripade prerusenia aplikacie sa tieto data nestratia
            //, ale naopak budu pristupne aj pre dalsie zariadenia, kedze sa ulozia do cloudu
            ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
            //roamingSettings.Values["userName"] = nameInput.Text;
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            Data.selectedIndex1 = lb.SelectedIndex;

            //if (selected == null)
            //{
            //    //napisat kod pre pripad, ak je vyber prazdny
            //}
            listBox2.Items.Clear();
            for (int i = Data.selectedIndex1 + 1; i < Data.numberOfStations; i++)
            {
                listBox2.Items.Add(_Rootobject.stations[i].name);
            }

        }

        private void listBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            Data.selectedIndex2 = lb.SelectedIndex;

            listView1.Items.Clear();

            listView1.Items.Add("Station " + "Arrival " + "Departure " + "Distance");

            for (int i = Data.selectedIndex1; i <= Data.selectedIndex2 + 1; i++)
            {
                listView1.Items.Add(
                    _Rootobject.stations[i].name + " " +
                    _Rootobject.stations[i].arrives + " " +
                    _Rootobject.stations[i].departs + " " +
                    _Rootobject.stations[i].distance);

            }

            //double price = listBox2.Items.
        }
    }
}
