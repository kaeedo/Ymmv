using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

using Ymmv.Models;
using Ymmv.Services;
using Ymmv.Views;

namespace Ymmv.ViewModels
{
    public class CarsViewModel : BaseViewModel
    {
        private Car _selectedCar;
        private readonly ICarStore _carStore;
        private readonly IFuelServiceStore _fuelServiceStore;

        public ObservableCollection<Car> Cars { get; }
        public Command LoadCarsCommand { get; }
        public Command AddCarCommand { get; }
        public Command EditCarCommand { get; }
        public Command DeleteCarCommand { get; }

        public CarsViewModel()
        {
            _carStore = DependencyService.Get<ICarStore>();
            _fuelServiceStore = DependencyService.Get<IFuelServiceStore>();

            Title = "All Cars";
            Cars = new ObservableCollection<Car>();

            LoadCarsCommand = new Command(async () => await ExecuteLoadCarsCommand());

            AddCarCommand = new Command(ExecureAddCarCommand);

            EditCarCommand = new Command<Car>(async car => await ExecuteEditCarCommand(car));

            DeleteCarCommand = new Command<Car>(async car => await ExecuteDeleteCarCommand(car));
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedCar = null;
        }

        public Car SelectedCar
        {
            get => _selectedCar;
            set
            {
                SetProperty(ref _selectedCar, value);
                OnCarSelected(value);
            }
        }
        private async Task ExecuteEditCarCommand(Car car)
        {
            var newCarPage = new NewCarPage(car);
            await Shell.Current.Navigation.PushModalAsync(newCarPage);
            await newCarPage.PageClosedTask;
            await ExecuteLoadCarsCommand();
        }

        private async Task ExecuteDeleteCarCommand(Car car)
        {
            var fuelServiceDeletes =
                (await _fuelServiceStore.GetFuelServicesForCarAsync(car.Id))
                .Select(fs => _fuelServiceStore.DeleteFuelServiceAsync(fs));

            await Task.WhenAll(fuelServiceDeletes);

            await  _carStore.DeleteCarAsync(car.Id);
            await ExecuteLoadCarsCommand();
        }

        private async Task ExecuteLoadCarsCommand()
        {
            IsBusy = true;

            try
            {
                Cars.Clear();
                SelectedCar = null;
                var cars = await _carStore.GetCarsAsync();
                

                foreach (var car in cars)
                {
                    var fuelServices = await _fuelServiceStore.GetFuelServicesForCarAsync(car.Id);
                    car.FuelServices = fuelServices;
                    Cars.Add(car);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void ExecureAddCarCommand(object obj)
        {
            var newCarPage = new NewCarPage();
            await Shell.Current.Navigation.PushModalAsync(newCarPage);
            await newCarPage.PageClosedTask;
            await ExecuteLoadCarsCommand();
        }

        private async void OnCarSelected(Car car)
        {
            if (car == null)
            {
                return;
            }

            await Shell.Current.GoToAsync($"{nameof(CarDetailsPage)}?{nameof(Car.Id)}={car.Id}");
        }
    }
}