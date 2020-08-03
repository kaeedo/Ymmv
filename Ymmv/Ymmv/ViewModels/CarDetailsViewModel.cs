using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Ymmv.Models;

namespace Ymmv.ViewModels
{
    public class CarDetailsViewModel : BaseViewModel
    {
        public Car Car { get; }

        public ObservableCollection<FuelService> FuelServices { get; }
        public Command LoadFuelServicesCommand { get; }

        public CarDetailsViewModel(Car car)
        {
            Car = car;
            Title = Car.Name;

            FuelServices = new ObservableCollection<FuelService>();

            LoadFuelServicesCommand = new Command(ExecuteLoadFuelServicesCommand);
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        private void ExecuteLoadFuelServicesCommand()
        {
            IsBusy = true;

            try
            {
                FuelServices.Clear();
                var fuelServices = Car.FuelServices;
                foreach (var fuelService in fuelServices)
                {
                    FuelServices.Add(fuelService);
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
    }
}
