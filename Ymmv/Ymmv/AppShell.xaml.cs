﻿using System;
using Ymmv.Views;
using Xamarin.Forms;

namespace Ymmv
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute($"{nameof(NewFuelServicePage)}", typeof(NewFuelServicePage));
            Routing.RegisterRoute($"{nameof(NewCarPage)}", typeof(NewCarPage));
            Routing.RegisterRoute($"{nameof(CarDetailsPage)}", typeof(CarDetailsPage));
        }

        private void OnMenuItemClicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
