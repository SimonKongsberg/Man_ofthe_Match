using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoM.Network;
using Xamarin.Forms;
using Plugin.Connectivity.Abstractions;
using Plugin.Connectivity;

namespace MoM
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

           MainPage = new NavigationPage(new MoM.MainPage());
           /* MainPage = CrossConnectivity.Current.IsConnected
        ? (Page)new MainPage()
        : new NoNetworkPage();*/

        }

		protected override void OnStart ()
		{
            // Handle when your app starts
            base.OnStart();
           /* CrossConnectivity.Current.ConnectivityChanged += HandleConnectivityChanged;

            void HandleConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
            {
                Type currentPage = this.MainPage.GetType();
                if (e.IsConnected && currentPage != typeof(MainPage))
                    this.MainPage = new MainPage();
                else if (!e.IsConnected && currentPage != typeof(NoNetworkPage))
                    this.MainPage = new NoNetworkPage();
            } */
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
