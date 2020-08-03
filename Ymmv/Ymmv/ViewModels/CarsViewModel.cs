using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        public ObservableCollection<Car> Cars { get; }
        public Command LoadCarsCommand { get; }
        public Command AddCarCommand { get; }
        public Command<Car> CarTapped { get; }

        public CarsViewModel()
        {
            _carStore = DependencyService.Get<ICarStore>();

            Title = "All Cars";
            Cars = new ObservableCollection<Car>();
            LoadCarsCommand = new Command(async () => await ExecuteLoadCarsCommand());

            CarTapped = new Command<Car>(OnCarSelected);

            AddCarCommand = new Command(OnAddCar);
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

        private async Task ExecuteLoadCarsCommand()
        {
            IsBusy = true;

            try
            {
                Cars.Clear();
                var cars = await _carStore.GetCarsAsync(true);
                foreach (var car in cars)
                {
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

        private async void OnAddCar(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewCarPage));
            await ExecuteLoadCarsCommand();
        }

        private async void OnCarSelected(Car car)
        {
            if (car == null)
            {
                return;
            }


            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(CarDetailsPage)}?{nameof(Car.Id)}={car.Id}");
        }
    }
}