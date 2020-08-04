using Ymmv.Models;
using Ymmv.Services;
using Xamarin.Forms;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Ymmv.ViewModels
{
    public class NewFuelServiceViewModel : BaseViewModel
    {
        private readonly ICarStore _carStore;

        private DistanceUnit _distanceUnit;
        private FuelUnit _fuelUnit;
        private double? _fuelAmount;
        private double? _distanceDriven;

        public Command TakePictureCommand { get; }
        public Command GalleryPictureCommand { get; }
        public Command CancelCommand { get; }
        public Command SaveCommand { get; }

        public FuelUnit FuelUnit
        {
            get => _fuelUnit;
            set => SetProperty(ref _fuelUnit, value);
        }

        public double? FuelAmount
        {
            get => _fuelAmount;
            set => SetProperty(ref _fuelAmount, value);
        }

        public List<string> FuelUnits
        {
            get => Enum.GetNames(typeof(FuelUnit)).ToList();
        }

        public DistanceUnit DistanceUnit
        {
            get => _distanceUnit;
            set => SetProperty(ref _distanceUnit, value);
        }

        public double? DistanceDriven
        {
            get => _distanceDriven;
            set => SetProperty(ref _distanceDriven, value);
        }

        public List<string> DistanceUnits
        {
            get => Enum.GetNames(typeof(DistanceUnit)).ToList();
        }

        public NewFuelServiceViewModel()
        {
            Title = "Add Fuel Service";

            _carStore = DependencyService.Get<ICarStore>();

            CancelCommand = new Command(OnCancel);
            SaveCommand = new Command(OnSave, ValidateSave);

            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return _fuelAmount > 0 && _distanceDriven > 0;
        }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var car = new Car
            {
            };

            await _carStore.AddCarAsync(car);

            await Shell.Current.GoToAsync("..");
        }
    }
}
