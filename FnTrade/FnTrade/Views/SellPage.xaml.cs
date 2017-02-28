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
                    var shares = e as Shares;
                    var confirmed = await DisplayAlert("Confirmation",
                                          $"Selling ({(shares).Quantity }) shares of {shares.Symbol}?",
                                          "Confirm", "Cancel");
                    if (confirmed) getDispatcher().ExecuteSell(shares);
                };
        }
    }
}