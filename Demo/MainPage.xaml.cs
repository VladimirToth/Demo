using Demo.Common;
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

            //this.PopulateListbox1();
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
            Data.selectedStation1 = lb.SelectedItem as Station;
            //if (selected == null)
            //{
            //    //napisat kod pre pripad, ak je vyber prazdny
            //}
            listBox2.Items.Clear();
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
                    rootObject.stations[i].tempDuration = rootObject.stations[i].duration - Data.selectedStation1.duration;

                    listView1.Items.Add(rootObject.stations[i]);
                }
                ////try
                ////{
               // for (int i = start; i <= end; i++)
              //  {
                 //   rootObject.stations[i].tempDuration = rootObject.stations[i].duration - Data.selectedStation1.duration;

//var dur = DepartureConverterHours(rootObject.stations[i].tempDuration);

//listView1.Items.Add(dur);
                //

              //  for (int i = start; i <= end; i++)
               // {
                  //  rootObject.stations[i].tempDuration = rootObject.stations[i].duration - Data.selectedStation1.duration;

                  //  var dur2 = DepartureConverterMinutes(rootObject.stations[i].tempDuration);

                 //   listView1.Items.Add(dur2);
             //   }
                //}catch(Exception ex)
                //{
                //    Windows.UI.Popups.MessageDialog message = new Windows.UI.Popups.MessageDialog(ex.StackTrace);
                //}
            }
            //PopulateListbox1();

            //double price = listBox2.Items.

        }


    //    private string DepartureConverterHours(int input)
     //   {
            // String startTime = "00";
         //   double minutes = input;
        //    int h = (int) (Math.Round((minutes/60), 2, MidpointRounding.ToEven));
           // double m = Math.Round((minutes % 60), 2, MidpointRounding.ToEven);
          //  double hours = Math.Round(h, 2, MidpointRounding.ToEven);
           // string s1 = String.Format("{0:F2}", h);
          //  int index = s1.IndexOf(".");
            //double ho = Math.
            
         //   string time = TimeSpan.FromHours(h).ToString();
            
          //  string hour = h.ToString().Substring(0, 2);
          //  string s1 = String.Format("{0}:00", hour);
          //  int hr = Int32.Parse(s1);

            //  String newDateStr = postFormater.format(dateObj);  

            //  string m = String.Format("%02s", ((long)(minutes % 60)).ToString().Substring(3, 3)); //+ Int32.Parse(startTime.Substring(3, 4)));
            // String newtime = String.Format("%02d:%02d", h, m);
            //  String newtime = h + ":" + m;


          //  return time;
              

            //String startTime = "00:00";
            //long minutes = duration;
            //int h =(int)(minutes / 60 + Int32.Parse(startTime.Substring(0, 1)));
            //int m = (int)(minutes % 60 + Int32.Parse(startTime.Substring(3, 4)));
            //String newtime = String.Format("%02d:%02d", h, m);
            //return newtime;
      //  }

     //   private int DepartureConverterMinutes(int input)
       // {
           // int minutes = input;
          //  double m = minutes % 60; //+ Int32.Parse(startTime.Substring(0, 1)));
           // double min = Math.Round(m, 2);
          //  string minute = (min.ToString()+"00").Substring(0,2); 

          //  int mn = Int32.Parse(minute);
            // double m = minutes % 60; //+ Int32.Parse(startTime.Substring(3, 4)));
            // String newtime = String.Format("%02d:%02d", h, m);
            //  String newtime = h + ":" + m;

         //   return mn;

       // }





    }
}
        
    


