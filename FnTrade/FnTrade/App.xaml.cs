using System;
using System.Collections.Generic;
using ManageTrades.ViewModels;
using Microsoft.FSharp.Collections;
using Xamarin.Forms;
using static Core.Entities;
using static Core.EventStore;
using static Integration.Factories;

namespace FnTrade
{
    public partial class App : Application
    {
        FSharpList<Tuple<Account, Events>> _eventStore = new FSharpList<Tuple<Account, Events>>(null, null);
        List<Shares> _account = new List<Shares>();

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new HomePage());
        }

        protected override void OnStart()
        {
            getDispatcher().BuyRequested +=
                async (s, e) => await MainPage.Navigation.PushAsync(
                    new BuyPage(new BuyViewModel(e as SharesInfo)));

            getDispatcher().ExecuteBuyRequested +=
                async (s, e) => await MainPage.Navigation.PushAsync(
                    new RequestBuyConfirmedPage(new RequestBuyConfirmedViewModel(e as RequestInfo)));

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
