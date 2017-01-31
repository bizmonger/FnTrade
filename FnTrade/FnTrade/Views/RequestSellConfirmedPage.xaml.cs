using ManageTrades.ViewModels;
using Xamarin.Forms;

namespace FnTrade
{
    public partial class RequestSellConfirmedPage : ContentPage
    {
        public RequestSellConfirmedPage(RequestSellConfirmedViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}