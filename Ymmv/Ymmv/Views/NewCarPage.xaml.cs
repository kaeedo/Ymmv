﻿
using Ymmv.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using Ymmv.Models;

namespace Ymmv.Views
{
    public partial class NewCarPage : ContentPage
    {
        NewCarViewModel _viewModel;

        public Task PageClosedTask => tcs.Task;

        private TaskCompletionSource<bool> tcs { get; set; } = new TaskCompletionSource<bool>();

        public NewCarPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new NewCarViewModel();
        }

        public NewCarPage(Car car)
        {
            InitializeComponent();

            BindingContext = _viewModel = new NewCarViewModel(car);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            tcs.SetResult(true);
        }

        public async Task PopAwaitableAsync()
        {
            await Navigation.PopAsync();
            tcs.SetResult(true);
        }
    }
}