
using Ymmv.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ymmv.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewCarPage : ContentPage
    {
        NewCarViewModel _viewModel;

        public NewCarPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new NewCarViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}