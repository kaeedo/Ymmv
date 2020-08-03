using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Ymmv.Services;
using Ymmv.ViewModels;

namespace Ymmv.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(SelectedCarId), nameof(Models.Car.Id))]
    public partial class CarDetailsPage : ContentPage
    {
        CarDetailsViewModel _viewModel;

        public string SelectedCarId
        {
            set
            {
                var carStore = DependencyService.Get<ICarStore>();
                var car = carStore.GetCarAsync(int.Parse(value)).GetAwaiter().GetResult();

                BindingContext = _viewModel ?? (_viewModel = new CarDetailsViewModel(car));
            }
        }

        public CarDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}