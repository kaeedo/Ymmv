
using Ymmv.ViewModels;
using Xamarin.Forms;
using Ymmv.Services;

namespace Ymmv.Views
{
    [QueryProperty(nameof(SelectedCarId), nameof(Models.Car.Id))]
    public partial class NewFuelServicePage : ContentPage
    {
        NewFuelServiceViewModel _viewModel;

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

                BindingContext = _viewModel = new NewFuelServiceViewModel(car);
            }
        }

        public NewFuelServicePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}