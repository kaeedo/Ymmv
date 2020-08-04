using Xamarin.Forms;
using Ymmv.Services;
using Ymmv.ViewModels;

namespace Ymmv.Views
{
    [QueryProperty(nameof(SelectedCarId), nameof(Models.Car.Id))]
    public partial class CarDetailsPage : ContentPage
    {
        CarDetailsViewModel _viewModel;

        public string SelectedCarId
        {
            set
            {
                if (_viewModel != null)
                {
                    return;
                }
                var carStore = DependencyService.Get<ICarStore>();
                var car = carStore.GetCarAsync(int.Parse(value)).GetAwaiter().GetResult();

                BindingContext = _viewModel = new CarDetailsViewModel(car);
            }
        }

        public CarDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}