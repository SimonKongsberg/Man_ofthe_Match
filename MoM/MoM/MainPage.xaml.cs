using MoM.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using DLToolkit.Forms.Controls;
using DLToolkit.Forms;

namespace MoM
{
	public partial class MainPage : ContentPage
	{
        readonly IList<Clubs> clubs = new ObservableCollection<Clubs>();
        readonly ClubsManager manager = new ClubsManager();

        public MainPage()
        {
            //ClubXamlList.FlowItemsSource = clubs;
            BindingContext = clubs;
            InitializeComponent();

            
        }

        async void BtnClub(object sender, ItemTappedEventArgs e)
        {
            var ClubItem = (Clubs)e.Item;

            var clubpage = new ClubPage
            {
                BindingContext = ClubItem
            };
            await Navigation.PushAsync(clubpage);

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            this.IsBusy = true;
            var clubCollection = await manager.GetAll();
            try
            {
                foreach (Clubs club in clubCollection)
                {
                    if (clubs.All(b => b.Name != club.Name))
                        clubs.Add(club);
                }
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        async void OnRefresh(object sender, EventArgs e)
        {
            this.IsBusy = true;
            var clubCollection = await manager.GetAll();
            try
            {
                foreach (Clubs club in clubCollection)
                {
                    if (clubs.All(b => b.Name != club.Name))
                        clubs.Add(club);
                }
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        async void OnAddNewClub(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(
                new AddEditClub(manager, clubs));
        }

        async void OnEditClub(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushModalAsync(
                new AddEditClub(manager, clubs, (Clubs)e.Item));
        }

        void HoverBtnClicked(object sender, EventArgs e)
        {
            HoverBtn.IsVisible = !HoverBtn.IsVisible;
        }

    }
}
