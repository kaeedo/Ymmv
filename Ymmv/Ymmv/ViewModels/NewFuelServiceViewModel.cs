using Ymmv.Models;
using Ymmv.Services;
using Xamarin.Forms;
using System.Collections.Generic;
using System;
using System.Linq;
using Xamarin.Forms.Internals;

namespace Ymmv.ViewModels
{
    public class NewFuelServiceViewModel : BaseViewModel
    {
        private readonly Car _car;
        private readonly IFuelServiceStore _fuelServiceStore;
        private DistanceUnit _distanceUnit;
        private FuelUnit _fuelUnit;
        private double? _fuelAmount;
        private double? _distanceDriven;
        private DateTime _fuelDate;

        public Command TakePictureCommand { get; }
        public Command GalleryPictureCommand { get; }
        public Command CancelCommand { get; }
        public Command SaveCommand { get; }

        public DateTime FuelDate
        {
            get => _fuelDate;
            set => SetProperty(ref _fuelDate, value);
        }

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

        public NewFuelServiceViewModel(Car car)
        {
            Title = "Add Fuel Service";

            _car = car;
            _fuelServiceStore = DependencyService.Get<IFuelServiceStore>();

            CancelCommand = new Command(OnCancel);
            SaveCommand = new Command(OnSave, ValidateSave);

            FuelDate = DateTime.Now;

            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return _fuelAmount > 0 && _distanceDriven > 0;
        }

        private async void OnCancel()
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

        private async void OnSave()
        {
            var fuelService = new FuelService
            {
                ServiceDate = _fuelDate,
                Kilometers = GetKilometers(),
                Liters = GetLiters(),
                CarId = _car.Id
            };

            await _fuelServiceStore.AddFuelServiceAsync(fuelService);

            await Shell.Current.Navigation.PopModalAsync();
        }

        private double GetKilometers()
        {
            if (_distanceUnit == DistanceUnit.Kilometers)
            {
                return _distanceDriven.Value;
            }

            return _distanceDriven.Value * 1.609344;
        }

        private double GetLiters()
        {
            if (_fuelUnit == FuelUnit.Liters)
            {
                return _fuelAmount.Value;
            }

            return _fuelAmount.Value * 3.7854;
        }
    }
}
