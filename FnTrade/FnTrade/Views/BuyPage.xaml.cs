using ManageTrades.ViewModels;
using Xamarin.Forms;

namespace FnTrade
{
    using static Core.Entities;
    using static Integration.Factories;

    public partial class BuyPage : ContentPage
    {
        public BuyPage(BuyViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;

            getDispatcher().ConfirmBuyRequested += async (s, e) =>
                {
                    var requestInfo = e as Shares;
                    var confirmed = await DisplayAlert("Confirmation",
                                          $"Buying ({(requestInfo).Quantity }) shares of {requestInfo.Symbol}?",
                                          "Confirm", "Cancel");
                    if (confirmed) getDispatcher().ExecuteBuy(requestInfo);
                };
        }
    }
}