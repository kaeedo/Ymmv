using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Ymmv.Services;
using Ymmv.Views;

namespace Ymmv
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<InMemoryCarStore>();
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
