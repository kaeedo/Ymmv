using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Ymmv.Models;
using Ymmv.Services;
using Ymmv.Views;

namespace Ymmv.ViewModels
{
    public class CarDetailsViewModel : BaseViewModel
    {
        private double _average;
        private double _median;
        private int _lifetimeKilometers;
        private int _lifetimeMiles;

        public Car Car { get; }

        private readonly IFuelServiceStore _fuelServiceStore;

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

        public int LifetimeKilometers
        {
            get => GetLifetimeKilometers();
            set => SetProperty(ref _lifetimeKilometers, value);
        }

        public int LifetimeMiles
        {
            get => GetLifetimeMiles();
            set => SetProperty(ref _lifetimeMiles, value);
        }

        public CarDetailsViewModel(Car car)
        {
            Car = car;
            Title = Car.Name;

            _fuelServiceStore = DependencyService.Get<IFuelServiceStore>();

            FuelServices = new ObservableCollection<FuelService>();
            FuelServices.CollectionChanged += OnFuelServicesChanged;
            
            LoadFuelServicesCommand = new Command(async () => await ExecuteLoadFuelServicesCommand());

            AddFuelServiceCommand = new Command(ExecuteAddFuelServiceCommand);
        }

        public async void OnAppearing()
        {
            IsBusy = true;
            await ExecuteLoadFuelServicesCommand();
        }

        private void OnFuelServicesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Average = CalculateAverage();
            Median = CalculateMedian();
            LifetimeKilometers = GetLifetimeKilometers();
            LifetimeMiles = GetLifetimeMiles();
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
                var fuelServices = (await _fuelServiceStore.GetFuelServicesForCarAsync(Car.Id)).OrderByDescending(fs => fs.ServiceDate);
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

        private int GetLifetimeKilometers()
        {
            if (FuelServices == null || !FuelServices.Any())
            {
                return 0;
            }

            return FuelServices.First().LifetimeKilometers;
        }

        private int GetLifetimeMiles()
        {
            if (FuelServices == null || !FuelServices.Any())
            {
                return 0;
            }

            return (int)Math.Round(FuelServices.First().LifetimeKilometers * 0.6213712);
        }
    }
}
