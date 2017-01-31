using ManageTrades.ViewModels;
using Xamarin.Forms;

namespace FnTrade
{
    public partial class RequestSellConfirmedPage : ContentPage
    {
        RequestSellConfirmedViewModel _viewModel;

        public RequestSellConfirmedPage(RequestSellConfirmedViewModel viewModel)
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