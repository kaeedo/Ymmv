﻿using System;
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
        private double _average;
        private double _median;

        public Car Car { get; }
        public ObservableCollection<FuelService> FuelServices { get; }
        public Command LoadFuelServicesCommand { get; }
        public Command AddFuelServiceCommand { get; }

        public double Average
        {
            get => CalculateAverage();
            set => SetProperty(ref _average, value);
        }

       

        public double Median
        {
            get => CalculateMedian();
            set => SetProperty(ref _median, value);
        }

        public CarDetailsViewModel(Car car)
        {
            Car = car;
            Title = Car.Name;

            FuelServices = new ObservableCollection<FuelService>();
            
            LoadFuelServicesCommand = new Command(async () => await ExecuteLoadFuelServicesCommand());

            AddFuelServiceCommand = new Command(ExecuteAddFuelServiceCommand);
        }

        public async void OnAppearing()
        {
            IsBusy = true;
            await ExecuteLoadFuelServicesCommand();
        }

        private async void ExecuteAddFuelServiceCommand(object obj)
        {
            var newFuelServicePage = new NewFuelServicePage(Car.Id);
            await Shell.Current.Navigation.PushModalAsync(newFuelServicePage);
            await newFuelServicePage.PageClosedTask;
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
                Average = CalculateAverage();
                Median = CalculateMedian();
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
        private double CalculateAverage()
        {
            if (FuelServices == null || !FuelServices.Any())
            {
                return 0.0;
            }

            return FuelServices.Sum(fs => fs.LitersPer100Kilometers) / FuelServices.Count;
        }

        private double CalculateMedian()
        {
            if (FuelServices == null || !FuelServices.Any())
            {
                return 0.0;
            }

            var values = FuelServices.Select(fs => fs.LitersPer100Kilometers).OrderBy(x => x).ToList();
            double mid = (values.Count - 1) / 2.0;
            return (values[(int)mid] + values[(int)(mid + 0.5)]) / 2;
        }
    }
}
