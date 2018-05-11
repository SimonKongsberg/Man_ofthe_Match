using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoM.ClubSub;
using MoM.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoM
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlayerPage : ContentPage
	{
		public PlayerPage ()
		{
			InitializeComponent ();
		}

        async void BtnTicket(object sender, ItemTappedEventArgs e)
        {
            var ClubItem = (Clubs)e.Item;

            var clubpage = new TicketPage
            {
                BindingContext = ClubItem
            };
            await Navigation.PushAsync(clubpage);

        }
    }
}