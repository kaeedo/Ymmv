using Xamarin.Forms;
using Ymmv.Models;
using Ymmv.Services;

namespace Ymmv.ViewModels
{
    public class CarDetailsViewModel : BaseViewModel
    {
        public Car Car { get; }

        public string ImagePreview { get; set; }

        public CarDetailsViewModel(Car car)
        {
            Car = car;
            Title = Car.Name;
            ImagePreview = Car.PictureFilePath;
        }
    }
}
