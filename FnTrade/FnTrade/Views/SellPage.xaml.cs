using ManageTrades.ViewModels;
using Xamarin.Forms;

namespace FnTrade
{
    public partial class SellPage : ContentPage
    {
        public SellPage(SellViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}