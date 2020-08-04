
using Ymmv.ViewModels;
using Xamarin.Forms;

namespace Ymmv.Views
{
    public partial class NewFuelServicePage : ContentPage
    {
        NewFuelServiceViewModel _viewModel;

        public NewFuelServicePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new NewFuelServiceViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}