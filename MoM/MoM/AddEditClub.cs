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

        readonly Clubs existingclub;
        readonly EntryCell nameCell, descriptionCell;
        readonly IList<Clubs> clubs;
        readonly ClubsManager manager;

        public AddEditClub(ClubsManager manager, IList<Clubs> clubs, Clubs existingclub = null)
            {
                this.manager = manager;
                this.clubs = clubs;
                this.existingclub = existingclub;

                var tableView = new TableView
                {
                    Intent = TableIntent.Form,
                    Root = new TableRoot(existingclub != null ? "Edit Club" : "New Club") {
                    new TableSection("Details") {

                        (nameCell = new EntryCell {
                            Label = "Name",
                            Placeholder = "add name",
                            Text = (existingclub != null) ? existingclub.Name : null,
                        }),
                        (descriptionCell = new EntryCell {
                            Label = "Cuisine",
                            Placeholder = "add description",
                            Text = (existingclub != null) ? existingclub.Description : null,
                        }),
                    },
                }
                };

                Button button = new Button()
                {
                    BackgroundColor = existingclub != null ? Color.Gray : Color.Green,
                    TextColor = Color.White,
                    Text = existingclub != null ? "Finished" : "Add Club",
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
                string cuisine = descriptionCell.Text;

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
                    if (existingclub != null)
                    {
                        existingclub.Name = name;
                        existingclub.Description = cuisine;

                        await manager.Update(existingclub);
                        int pos = clubs.IndexOf(existingclub);
                        clubs.RemoveAt(pos);
                        clubs.Insert(pos, existingclub);
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
