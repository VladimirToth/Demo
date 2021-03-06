﻿using Demo.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using EASendMailRT;



// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Demo
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class PayTicket : Page
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

        
        public PayTicket()
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

        private void Confirm_Ticket_Click(object sender, RoutedEventArgs e)
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");

        }

        private void combBoxSeats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            string item = cb.SelectedItem as string;

            switch (item)
            {
                case "1 seat":
                    txtTotalPrice.Text = (Data.price * 1).ToString() + " YUANS";
                    break;
                case "2 seats":
                    txtTotalPrice.Text = (Data.price * 2).ToString() + " YUANS";
                    break;
                case "3 seats":
                    txtTotalPrice.Text = (Data.price * 3).ToString() + " YUANS";
                    break;
                case "4 seats":
                    txtTotalPrice.Text = (Data.price * 4).ToString() + " YUANS";
                    break;
                case "5 seats":
                    txtTotalPrice.Text = (Data.price * 5).ToString() + " YUANS";
                    break;
                default:
                    txtTotalPrice.Text = "Please, choose seats";
                    break;
            }
        }

        private async void btnSenda_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                txtName.Focus(FocusState.Programmatic);
                return;
            }

            btnSend.IsEnabled = false;
            await Send_Email();
            btnSend.IsEnabled = true;
        }

        private async Task Send_Email()
        {
            String Result = "";
            string email = txtBoxEmail.Text;
            string name = txtName.Text;
            string seats = combBoxSeats.SelectedItem as string;

            if (seats != null)
            {
                
                Random rd = new Random();
                int number = rd.Next(100000, 999999);

                try
                {
                    SmtpMail oMail = new SmtpMail("TryIt");
                    SmtpClient oSmtp = new SmtpClient();

                    oMail.From = new MailAddress("trainscheduleconfirmation@gmail.com");

                    oMail.To.Add(new MailAddress(email));

                    oMail.Subject = "Train confirmation: " + number;

                    oMail.TextBody = "Dear " + name + "," + Environment.NewLine + "your order was confirmed." + Environment.NewLine +
                        Environment.NewLine + "Please, save your confirmation number: " + number;

                    SmtpServer oServer = new SmtpServer("smtp.gmail.com");
          
                    oServer.User = "trainscheduleconfirmation@gmail.com";
                    oServer.Password = "abc123456789def";

                    oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                    await oSmtp.SendMailAsync(oServer, oMail);
                    Result = "Thank You!" + Environment.NewLine + "Your order has been successfully completed. You should receive a confirmation email shortly!";
                }
                catch
                {
                    Result = String.Format("Please, write your email.");
                }

                Windows.UI.Popups.MessageDialog dlg = new
                    Windows.UI.Popups.MessageDialog(Result);

                await dlg.ShowAsync();
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}