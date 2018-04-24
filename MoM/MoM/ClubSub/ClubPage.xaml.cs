﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoM.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoM
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClubPage : ContentPage
    {
        
		public ClubPage ()
		{
			InitializeComponent ();
            
        }

        async void OnNavigateButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}