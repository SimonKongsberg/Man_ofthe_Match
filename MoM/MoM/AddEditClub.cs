using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoM.Network;
using Xamarin.Forms;
using MoM.Data;


namespace MoM
{
	public class AddEditClub : ContentPage
	{

        readonly Clubs existingBook;
        readonly EntryCell nameCell, cuisineCell;
        readonly IList<Clubs> clubs;
        readonly ClubsManager manager;

        public AddEditClub(ClubsManager manager, IList<Clubs> clubs, Clubs existingBook = null)
            {
                this.manager = manager;
                this.clubs = clubs;
                this.existingBook = existingBook;

                var tableView = new TableView
                {
                    Intent = TableIntent.Form,
                    Root = new TableRoot(existingBook != null ? "Edit Club" : "New Club") {
                    new TableSection("Details") {

                        (nameCell = new EntryCell {
                            Label = "Title",
                            Placeholder = "add title",
                            Text = (existingBook != null) ? existingBook.Name : null,
                        }),
                        (cuisineCell = new EntryCell {
                            Label = "Genre",
                            Placeholder = "add genre",
                            Text = (existingBook != null) ? existingBook.Cuisine : null,
                        }),
                    },
                }
                };

                Button button = new Button()
                {
                    BackgroundColor = existingBook != null ? Color.Gray : Color.Green,
                    TextColor = Color.White,
                    Text = existingBook != null ? "Finished" : "Add Book",
                    CornerRadius = 0,
                };
                button.Clicked += OnDismiss;

                Content = new StackLayout
                {
                    Spacing = 0,
                    Children = { tableView, button },
                };
            }
        async void OnDismiss(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;
            this.IsBusy = true;
            try
            {
                string name = nameCell.Text;
                string cuisine = cuisineCell.Text;

                if (string.IsNullOrWhiteSpace(name)
                    || string.IsNullOrWhiteSpace(cuisine))
                {
                    this.IsBusy = false;
                    await this.DisplayAlert("Missing Information",
                        "You must enter values.",
                        "OK");
                }
                else
                {
                    if (existingBook != null)
                    {
                        existingBook.Name = name;
                        existingBook.Cuisine = cuisine;

                        await manager.Update(existingBook);
                        int pos = clubs.IndexOf(existingBook);
                        clubs.RemoveAt(pos);
                        clubs.Insert(pos, existingBook);
                    }
                    else
                    {
                        Clubs club = await manager.Add(name, cuisine);
                        clubs.Add(club);
                    }

                    await Navigation.PopModalAsync();
                }

            }
            finally
            {
                this.IsBusy = false;
                button.IsEnabled = true;
            }
        }


    }
}
