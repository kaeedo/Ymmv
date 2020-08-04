using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Ymmv.Models;
using Ymmv.Views;

namespace Ymmv.ViewModels
{
    public class CarDetailsViewModel : BaseViewModel
    {
        public Car Car { get; }
        public ObservableCollection<FuelService> FuelServices { get; }
        public Command LoadFuelServicesCommand { get; }
        public Command AddFuelServiceCommand { get; }

        public CarDetailsViewModel(Car car)
        {
            Car = car;
            Title = Car.Name;

            FuelServices = new ObservableCollection<FuelService>();
            
            LoadFuelServicesCommand = new Command(async () => await ExecuteLoadFuelServicesCommand());

            AddFuelServiceCommand = new Command(async () => await ExecuteAddFuelServiceCommand());
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        private async Task ExecuteAddFuelServiceCommand()
        {
            await Shell.Current.GoToAsync($"{nameof(NewFuelServicePage)}");
            await ExecuteLoadFuelServicesCommand();
        }

        private async Task ExecuteLoadFuelServicesCommand()
        {
            IsBusy = true;

            try
            {
                FuelServices.Clear();
                var fuelServices = await Task.FromResult(Car.FuelServices);
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
