using ManageTrades.ViewModels;
using Xamarin.Forms;
using static Core.Entities;
using static Integration.Factories;

namespace FnTrade
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new HomePage());
        }

        protected override void OnStart()
        {
            getDispatcher().SellRequested +=
                async (s, e) => await MainPage.Navigation.PushAsync(
                    new SellPage(new SellViewModel(e as SharesInfo)));

            getDispatcher().ExecuteSellRequested +=
                async (s, e) => await MainPage.Navigation.PushAsync(
                    new RequestSellConfirmedPage(new RequestSellConfirmedViewModel(e as RequestInfo)));
        }

        protected override void OnSleep() { }
        protected override void OnResume() { }
    }
}
