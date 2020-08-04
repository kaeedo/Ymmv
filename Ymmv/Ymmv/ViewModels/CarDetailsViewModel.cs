using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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

        public double Average 
        { 
            get
            {
                if (Car.FuelServices == null || !Car.FuelServices.Any())
                {
                    return 0.0;
                }

                return Car.FuelServices.Sum(fs => fs.LitersPer100Kilometers) / Car.FuelServices.Count;
            } 
        }

        public double Median
        {
            get
            {
                if (Car.FuelServices == null || !Car.FuelServices.Any())
                {
                    return 0.0;
                }

                var values = Car.FuelServices.Select(fs => fs.LitersPer100Kilometers).OrderBy(x => x).ToList();
                double mid = (values.Count - 1) / 2.0;
                return (values[(int)(mid)] + values[(int)(mid + 0.5)]) / 2;

            }
        }

        public CarDetailsViewModel(Car car)
        {
            Car = car;
            Title = Car.Name;

            FuelServices = new ObservableCollection<FuelService>();
            
            LoadFuelServicesCommand = new Command(async () => await ExecuteLoadFuelServicesCommand());

            AddFuelServiceCommand = new Command(ExecuteAddFuelServiceCommand);
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        private async void ExecuteAddFuelServiceCommand(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(NewFuelServicePage)}?{nameof(Car.Id)}={Car.Id}");
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
