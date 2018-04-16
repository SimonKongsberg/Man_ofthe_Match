using MoM.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoM
{
	public partial class MainPage : ContentPage
	{
        readonly IList<Clubs> clubs = new ObservableCollection<Clubs>();
        readonly ClubsManager manager = new ClubsManager();

        public MainPage()
        {
            BindingContext = clubs;
            InitializeComponent();
        }

        async void OnRefresh(object sender, EventArgs e)
        {
            this.IsBusy = true;
            var bookCollection = await manager.GetAll();
            try
            {
                foreach (Clubs club in bookCollection)
                {
                    if (clubs.All(b => b.name != club.name))
                        clubs.Add(club);
                }
            }
            finally
            {
                this.IsBusy = false;
            }
        }
    }
}
