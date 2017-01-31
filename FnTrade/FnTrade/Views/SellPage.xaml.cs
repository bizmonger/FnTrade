using ManageTrades.ViewModels;
using Xamarin.Forms;

namespace FnTrade
{
    using static Core.Entities;
    using static Integration.Factories;

    public partial class SellPage : ContentPage
    {
        public SellPage(SellViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;

            getDispatcher().ConfirmSellRequested += async (s, e) =>
            {
                var requestInfo = e as RequestInfo;
                var confirmed = await DisplayAlert("Confirm!",
                                      $"Sell {(requestInfo).Quantity } shares of {requestInfo.Symbol}",
                                      "Confirm", "Cancel");
                if (confirmed) getDispatcher().ExecuteSell(requestInfo);
            };
        }
    }
}