﻿using Xamarin.Forms;
using static Integration.Factories;

namespace FnTrade
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new HomePage());

            var dispatcher = getDispatcher();
            dispatcher.SellRequested += (s, e) => MainPage.Navigation.PushAsync(new SellPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
