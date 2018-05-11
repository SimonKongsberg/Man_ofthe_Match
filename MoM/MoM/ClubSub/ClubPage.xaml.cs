using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MoM.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoM
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClubPage : ContentPage
    {
        readonly IList<Clubs> clubsMatches = new ObservableCollection<Clubs>();
        readonly ClubsManager manager = new ClubsManager();

        public ClubPage ()
		{
            BindingContext = clubsMatches;
            InitializeComponent ();
            
        }

        async void OnNavigateButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void OnRefresh(object sender, EventArgs e)
        {
            this.IsBusy = true;
            var bookCollection = await manager.GetAll();
            try
            {
                foreach (Clubs club in bookCollection)
                {
                    if (clubsMatches.All(b => b.Name != club.Name))
                        clubsMatches.Add(club);
                }
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        async void BtnPlayerPage(object sender, ItemTappedEventArgs e)
        {
            var ClubItem = (Clubs)e.Item;

            var clubpage = new PlayerPage
            {
                BindingContext = ClubItem
            };
            await Navigation.PushAsync(clubpage);

        }

        // Searchbar start

        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new Command<string>((text) =>
                {
                    // The text parameter can now be used for searching.
                }));
            }
        }

        public class TextChangedBehavior : Behavior<SearchBar>
        {
            protected override void OnAttachedTo(SearchBar bindable)
            {
                base.OnAttachedTo(bindable);
                bindable.TextChanged += Bindable_TextChanged;
            }

            protected override void OnDetachingFrom(SearchBar bindable)
            {
                base.OnDetachingFrom(bindable);
                bindable.TextChanged -= Bindable_TextChanged;
            }

            private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
            {
                ((SearchBar)sender).SearchCommand?.Execute(e.NewTextValue);
            }
        }
        // Searchbar end


        // "Dropdown"
        void DiviButtonClicked(object sender, EventArgs e)
        {
            Divi.IsVisible = !Divi.IsVisible;
        }

        void KomKaButtonClicked(object sender, EventArgs e)
        {
            KomKa.IsVisible = !KomKa.IsVisible;
        }

        void AfKaButtonClicked(object sender, EventArgs e)
        {
            AfKa.IsVisible = !AfKa.IsVisible;
        }
        

    }
}