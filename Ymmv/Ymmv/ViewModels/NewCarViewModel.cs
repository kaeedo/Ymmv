
using System;
using System.Threading.Tasks;
using Ymmv.Models;
using Ymmv.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace Ymmv.ViewModels
{
    public class NewCarViewModel : BaseViewModel
    {
        private string _make;
        private string _model;
        private string _imagePreview;
        private string _saveButtonText;
        private readonly ICarStore _carStore;
        private Car _carToEdit;

        public Command TakePictureCommand { get; }
        public Command GalleryPictureCommand { get; }
        public Command CancelCommand { get; }
        public Command SaveCommand { get; }

        public string Make
        {
            get => _make; 
            set => SetProperty(ref _make, value);
        }

        public string Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }

        public string ImagePreview
        {
            get => _imagePreview;
            set => SetProperty(ref _imagePreview, value);
        }

        public string SaveButtonText
        {
            get => _saveButtonText;
            set => SetProperty(ref _saveButtonText, value);
        }

        public NewCarViewModel()
        {
            Title = "Add Car";
            _saveButtonText = "Save";

            _carStore = DependencyService.Get<ICarStore>();

            TakePictureCommand = new Command(TakePicture);
            GalleryPictureCommand = new Command(GalleryPicture);
            CancelCommand = new Command(OnCancel);
            SaveCommand = new Command(OnSave, ValidateSave);


            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public NewCarViewModel(Car car)
        {
            Title = "Edit Car";
            _saveButtonText = "Update";

            _carStore = DependencyService.Get<ICarStore>();
            _carToEdit = car;

            Make = _carToEdit.Make;
            Model = _carToEdit.Model;
            ImagePreview = _carToEdit.PictureFilePath;

            TakePictureCommand = new Command(TakePicture);
            GalleryPictureCommand = new Command(GalleryPicture);
            CancelCommand = new Command(OnCancel);
            SaveCommand = new Command(OnUpdate, ValidateSave);

            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(_make) && !string.IsNullOrWhiteSpace(_model);
        }

        private async void TakePicture()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Shell.Current.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "CarPictures",
                Name = $"{Guid.NewGuid()}.jpg"
            });

            ImagePreview = file.Path;
        }

        private async void GalleryPicture()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Shell.Current.DisplayAlert("No Gallery", ":( No gallery available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions());

            ImagePreview = file.Path;
        }

        private async void OnCancel()
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

        private async void OnSave()
        {
            var car = new Car
            {
                Make = Make,
                Model = Model,
                PictureFilePath = ImagePreview
            };

            await _carStore.AddCarAsync(car);

            await Shell.Current.Navigation.PopModalAsync();
        }

        private async void OnUpdate()
        {
            _carToEdit.Make = Make;
            _carToEdit.Model = Model;
            _carToEdit.PictureFilePath = ImagePreview;

            await _carStore.UpdateCarAsync(_carToEdit);

            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}
