
using Ymmv.ViewModels;
using Xamarin.Forms;
using Ymmv.Services;
using System.Threading.Tasks;

namespace Ymmv.Views
{
    //[QueryProperty(nameof(SelectedCarId), nameof(Models.Car.Id))]
    public partial class NewFuelServicePage : ContentPage
    {
        NewFuelServiceViewModel _viewModel;

        public Task PageClosedTask => tcs.Task;

        private TaskCompletionSource<bool> tcs { get; set; } = new TaskCompletionSource<bool>();

        public NewFuelServicePage(int carId)
        {
            InitializeComponent();

            var carStore = DependencyService.Get<ICarStore>();
            var car = carStore.GetCarAsync(carId).GetAwaiter().GetResult();

            BindingContext = _viewModel = new NewFuelServiceViewModel(car);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            tcs.SetResult(true);
        }

        public async Task PopAwaitableAsync()
        {
            await Navigation.PopAsync();
            tcs.SetResult(true);
        }
    }
}