using ManageTrades.ViewModels;
using Xamarin.Forms;

namespace FnTrade
{
    public partial class RequestBuyConfirmedPage : ContentPage
    {
        RequestBuyConfirmedViewModel _viewModel;

        public RequestBuyConfirmedPage(RequestBuyConfirmedViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.Load();
        }
    }
}