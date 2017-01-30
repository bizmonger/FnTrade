using ManageTrades.ViewModels;
using Xamarin.Forms;
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
                async (s, e) => await MainPage.Navigation.PushAsync(new SellPage(new SellViewModel(e as string)));
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
