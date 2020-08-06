using System;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Ymmv.Models;
using Ymmv.Services;
using Ymmv.Views;

namespace Ymmv
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<CarStore>();
            DependencyService.Register<FuelServiceStore>();

            var db = new SQLiteConnection(Constants.DatabaseFilePath);

            db.CreateTable<Car>();
            db.CreateTable<FuelService>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
