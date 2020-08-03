using Ymmv.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ymmv.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarsPage : ContentPage
    {
        CarsViewModel _viewModel;

        public CarsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new CarsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}