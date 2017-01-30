using Home.ViewModels;
using Xamarin.Forms;

namespace FnTrade
{
    public partial class HomePage : ContentPage
    {
        HomeViewModel _viewModel;

        public HomePage()
        {
            InitializeComponent();
            _viewModel = BindingContext as HomeViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.Load();
        }
    }
}